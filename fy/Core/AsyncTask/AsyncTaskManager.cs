using Modobay.Api;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modobay.AsyncTask
{
    public class AsyncTaskManager
    {
        static AsyncTaskManager()
        {
        }

        private static string GetTaskCacheKey(string api, AsyncTaskAttribute asyncTask,string userId)
        {
            var flag = string.Format("{0}", userId);
            var cacheKey = $"AsyncTask:{api}:{flag}";
            return cacheKey;
        }

        #region 客户端交互

        public static List<TaskInfo> GetTaskInfoList(string api,string userId)
        {
            var list = new List<TaskInfo>();
            var flag = string.Format("{0}", userId);
            var cacheKey = $"AsyncTask:{api}:{flag}";
            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var cacheResult = redisApiClient.Get2<TaskInfo>(cacheKey);
            if (cacheResult != null)
            {
                list.Add(cacheResult);
                if (cacheResult.Done == 100)
                {
                    redisApiClient.Remove(cacheKey);
                }
                else
                {
                    // 超过n分钟没有更新进度的，删除任务
                    if (cacheResult.UpdateTime == null || (DateTime.Now - cacheResult.UpdateTime.Value) > TimeSpan.FromMinutes(5))
                    {
                        redisApiClient.Remove(cacheKey);
                    }
                }
            }
            return list;
        }

        internal static TaskInfo GetTaskInfo(ActionExecutingContext context,string userId)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var asyncTasks = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AsyncTaskAttribute), true);
            if (asyncTasks.Count() <= 0) return null;
            var asyncTask = asyncTasks[0] as AsyncTaskAttribute;
            var cacheKey = GetTaskCacheKey(actionDescriptor.AttributeRouteInfo.Template, asyncTask,userId);

            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var cacheResult = redisApiClient.Get2<TaskInfo>(cacheKey);
            if (cacheResult == null)
            {
                cacheResult = NewTask(cacheKey, context, redisApiClient, actionDescriptor, null);
            }
            if (cacheResult.Done == 100)
            {
                redisApiClient.Remove(cacheKey);
            }
            else
            {
                // 超过n分钟没有更新进度的，重建任务
                if (cacheResult.UpdateTime == null || (DateTime.Now - cacheResult.UpdateTime.Value) > TimeSpan.FromMinutes(5))
                {
                    cacheResult = NewTask(cacheKey, context, redisApiClient, actionDescriptor, "任务已重启（原因：超过5分钟未更新任务进度）");
                }
            }

            return cacheResult;
        }

        private static TaskInfo NewTask(string cacheKey, ActionExecutingContext context, IRedisApiClient redisApiClient, ControllerActionDescriptor actionDescriptor, string remark)
        {
            var cacheResult = new TaskInfo()
            {
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Remark = remark,
                TaskId = cacheKey
            };
            redisApiClient.Set2(cacheKey, cacheResult, TimeSpan.FromDays(1));
            
            Task.Run(async () =>
            {
                //AppManager.CurrentAppContext.TaskId = cacheKey;
                await Task.Delay(1);
                try
                {
                    var result = actionDescriptor.MethodInfo.Invoke(context.Controller, context.ActionArguments.Values.ToArray());
                }
                catch (Exception ex)
                {
                    Lib.Log.WriteExceptionLog($"NewTask Exception:{ex.Message}  <br> StackTrace:{ex.StackTrace}");
                }
            });
            return cacheResult;
        }

        #endregion

        #region Api交互

        public static void BeginTask(string taskId, int count)
        {
            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var cacheKey = taskId;
            var cacheResult = redisApiClient.Get2<TaskInfo>(cacheKey);
            if (cacheResult == null) throw new AppException("未找到指定的异步任务");
            cacheResult.Count = count;
            cacheResult.UpdateTime = DateTime.Now;
            redisApiClient.Set2(cacheKey, cacheResult, TimeSpan.FromDays(1));
        }

        public static void SetTaskInfo(string taskId, int currentIndex, string currentInfo)
        {
            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var cacheKey = taskId;
            var cacheResult = redisApiClient.Get2<TaskInfo>(cacheKey);
            if (cacheResult == null) throw new AppException("未找到指定的异步任务");
            cacheResult.CurrentIndex = currentIndex;
            cacheResult.CurrentInfo = currentInfo;
            try
            {
                cacheResult.Done = (int)(cacheResult.CurrentIndex / (decimal)cacheResult.Count * 100m);
            }
            catch (Exception ex) { }
            cacheResult.UpdateTime = DateTime.Now;
            redisApiClient.Set2(cacheKey, cacheResult, TimeSpan.FromDays(1));
        }

        public static void EndTask(string taskId, object result)
        {
            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var cacheKey = taskId;
            var cacheResult = redisApiClient.Get2<TaskInfo>(cacheKey);
            if (cacheResult == null) throw new AppException("未找到指定的异步任务");
            cacheResult.Data = result;
            cacheResult.UpdateTime = DateTime.Now;
            redisApiClient.Set2(cacheKey, cacheResult, TimeSpan.FromDays(1));

            // todo 异步任务完成，推送系统消息
        }

        #endregion


        #region 简单异步任务

        public static async Task NewSimpleTask<T>(Delegate func, string requestId, params object[] input)
        {
            if (string.IsNullOrEmpty(requestId)) return;
            Task.Run(async () =>
            {
                var taskInfo = $"requestId:{requestId}  input:{input?.Length}";
                await Task.Delay(1);                
                try
                {
                    var result = (T)func.DynamicInvoke(input);
                    if (result != null)
                    {
                        var cacheKey = $"AsyncTask:0SimpleTask:{requestId.Replace(':', '_')}";
                        var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
                        redisApiClient.Set2(cacheKey, result, TimeSpan.FromHours(2));
                    }
                }
                catch (TargetInvocationException ex)
                {
                    Lib.Log.WriteExceptionLog($"NewSimpleTask {taskInfo} Exception:{ex.Message}  <br> StackTrace:{ex.StackTrace}");
                    if (ex.InnerException != null)
                    {
                        Lib.Log.WriteExceptionLog($"NewSimpleTask {taskInfo} Exception:{ex.InnerException.Message}  <br> StackTrace:{ex.InnerException.StackTrace}");
                    }
                }
                catch (Exception ex)
                {
                    Lib.Log.WriteExceptionLog($"NewSimpleTask {taskInfo} Exception:{ex.Message}  <br> StackTrace:{ex.StackTrace}");
                }
            });
        }

        public static dynamic GetSimpleTaskResult(string requestId)
        {
            var cacheKey = $"AsyncTask:0SimpleTask:{requestId.Replace(':', '_')}";
            var redisApiClient = Modobay.Cache.CacheManager.RedisCache;
            var result = redisApiClient.Get2<dynamic>(cacheKey);
            return result;
        }

        #endregion
    }
}
