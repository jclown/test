using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Modobay
{
    public class ApiPermission : IApiPermission
    {
        private readonly Modobay.IAppContext app;
        public const string CorpAdmin = "CorpAdmin";

        public ApiPermission(Modobay.IAppContext appContext)
        {
            //Lib.Log.WritePerformanceLog($"ApiPermission:{DateTime.Now.ToString("ss.fff")}");
            this.app = appContext;
        }

        /// <summary>
        /// 检查用户的api权限。
        /// </summary>
        /// <param name="permissionCode"></param>
        //[Modobay.Api.NonAction]
        public void CheckUserApiPermission(string permissionCode)
        {
            //permissionCode = $"{app.AppID}:{permissionCode}";
            //if (!app.User.Permissions.Contains(CorpAdmin) && !app.User.Permissions.Contains(permissionCode)) throw new AppException(AppExceptionType.UserAccessDenied);
        }

        /// <summary>
        /// 检查应用的api权限。
        /// </summary>
        /// <param name="api"></param>
        //[Modobay.Api.NonAction]
        public void CheckAppApiPermission(string api)
        {
            //var denyApiList = Modobay.ConfigManager.AppApiDenyConfig[app.AppID];
            //if (denyApiList.Contains(api))
            //{
            //    throw new Modobay.AppException(Modobay.AppExceptionType.AccessDenied);
            //}

            //foreach (var denyApi in denyApiList)
            //{
            //    if (string.IsNullOrEmpty(denyApi)) continue;
            //    var reg = new Regex(denyApi);
            //    if (reg.IsMatch(api)) throw new Modobay.AppException(Modobay.AppExceptionType.AccessDenied);
            //}
        }
    }
}
