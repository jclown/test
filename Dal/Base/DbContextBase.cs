using Modobay;
using Modobay.Cache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dal
{
    public class DbContextBase : DbContext
    {
        protected Modobay.IAppContext app
        {
            get
            {
                return Modobay.AppManager.CurrentAppContext;
            }
        }

        private static Dictionary<string, dynamic> _queryFilters;

        static DbContextBase()
        {
            var configStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Dal.queryFilters.json");
            _queryFilters = Modobay.ConfigManager.GetConfig(configStream);
        }

        public DbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //if (Modobay.AppManager.IsEnableDatabaseLog) modelBuilder.EnableAutoHistory(2048);
            SetQueryFilter(modelBuilder, modelBuilder.Model.GetEntityTypes().ToList());
            base.OnModelCreating(modelBuilder);
        }

        protected virtual void OnModelCreatingExt(ModelBuilder modelBuilder)
        { 
        }

        #region 数据隔离：过滤、检查、获取

        const string IsDeleteColumnName = "IsDeleted";
        
        private void SetQueryFilter(ModelBuilder modelBuilder, IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType> tables)
        {
            var app = Modobay.AppManager.CurrentAppContext;
            var userId = app?.User?.UserID;// ?? -1;
            var corpId = app?.User?.ShopId;// ?? -1;

            foreach (var table in tables)
            {
                var expressionList = new List<BinaryExpression>();
                var parameterExpression = Expression.Parameter(table.ClrType, "e");

                try
                {
                    if (table.FindProperty(IsDeleteColumnName) != null)
                    {
                        expressionList.Add(ExpressionHelper.Build(parameterExpression, IsDeleteColumnName, false));
                    }

                    if (!_queryFilters.ContainsKey(table.Name)) continue;
                    var filter = _queryFilters[table.Name];
                    if (string.IsNullOrEmpty(filter.AppID)) continue;

                    string[] appidList = filter.AppID.Split('|');
                    string corpColumn = filter.CorpFilter;
                    string userColumn = filter.UserFilter;
                    //string appColumn = filter.AppFilter;

                    if (filter.AppID != "*")    // 指定具体appid时则需匹配对应过滤的字段
                    {
                        if (!appidList.Contains(app.AppID)) continue;
                        var columnIndex = Array.IndexOf(appidList, app.AppID);

                        var corpColumnList = filter.CorpFilter.Split('|');
                        var corpColumnIndex = (corpColumnList.Length == appidList.Length) ? columnIndex : 0;
                        corpColumn = corpColumnList[corpColumnIndex];

                        var userColumnList = filter.UserFilter.Split('|');
                        var userColumnIndex = (userColumnList.Length == appidList.Length) ? columnIndex : 0;
                        userColumn = userColumnList[userColumnIndex];
                    }

                    // 企业用户/个人用户过滤规则
                    if (!string.IsNullOrEmpty(corpColumn) && !string.IsNullOrEmpty(userColumn))
                    {
                        // 两个过滤规则同时存在的，是为了兼容企业用户和个人用户（需要特殊处理的情况是个人用户后期绑定到企业，原先个人的数据仍然可见，即corpid=0）                       
                        if (corpId == 0)
                        {
                            expressionList.Add(ExpressionHelper.Build(parameterExpression, userColumn, userId));
                        }
                        else if (corpId > 0)
                        {
                            expressionList.Add(ExpressionHelper.Build(parameterExpression, corpColumn, corpId));
                        }
                    }
                    else if (!string.IsNullOrEmpty(corpColumn))
                    {
                        expressionList.Add(ExpressionHelper.Build(parameterExpression, corpColumn, corpId));
                    }
                    else if (!string.IsNullOrEmpty(userColumn))
                    {
                        expressionList.Add(ExpressionHelper.Build(parameterExpression, userColumn, userId));
                    }

                    // todo 待补充appid过滤规则，与登录状态无关
                }
                finally
                {
                    var expression = ExpressionHelper.Build(expressionList, parameterExpression);
                    if (expression != null)
                    {
                        modelBuilder.Entity(table.ClrType).HasQueryFilter(expression);
                    }
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            if (Modobay.AppManager.IsEnableDatabaseLog) this.EnsureAutoHistory();
            // todo 保存时的数据隔离规则检查
            //foreach (var entry in ChangeTracker.Entries<Post>())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.CurrentValues["IsDeleted"] = false;
            //            break;
            //        case EntityState.Deleted:
            //            entry.State = EntityState.Modified;
            //            entry.CurrentValues["IsDeleted"] = true;
            //            break;
            //    }
            //}
        }

        //public override void Dispose()
        //{
        //    "".ToString();
        //    //base.Dispose();
        //}


        /// <summary>
        /// 调用Find方法获取数据，并检查以下情况，符合则抛异常：1、id小于等于0 2、找不到记录。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByKey<T>(int id) where T : class
        {
            if (id <= 0) throw new Modobay.AppException(Modobay.AppExceptionType.ParameterError);
            var item = Find<T>(id);
            if (item == null) throw new Modobay.AppException(Modobay.AppExceptionType.NotFound);
            return item;
        }

        #endregion


        #region 创建并设置默认值

        /// <summary>
        /// 按默认值创建对象，并附加到dbset。默认值包括空字符串，创建时间，修改时间，删除标识。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attachToDbSet">默认会附加到dbset，可设置false不附加。</param>
        /// <returns></returns>
        public T Create<T>(bool attachToDbSet = true) where T : class
        {
            var type = typeof(T);
            DbSet<T> dbSet = Set<T>();
            var item = System.Activator.CreateInstance<T>();

            var createdTimeProperty = type.GetProperty("CreateTime");
            if (createdTimeProperty != null)
            {
                createdTimeProperty.SetValue(item, DateTime.Now);
            }
            var createdDateProperty = type.GetProperty("CreatedDate");
            if (createdDateProperty != null)
            {
                createdDateProperty.SetValue(item, DateTime.Now);
            }

            var creatorIDProperty = type.GetProperty("CreateUserID");            
            if (creatorIDProperty != null && app?.User?.UserID > 0)
            {
                creatorIDProperty.SetValue(item, app?.User?.UserID);
            }

            var modifiedTimeProperty = type.GetProperty("UpdateTime");
            if (modifiedTimeProperty != null)
            {
                modifiedTimeProperty.SetValue(item, DateTime.Now);
            }
                        
            var modifierIDProperty = type.GetProperty("UpdateUserID");
            if (modifierIDProperty != null && app?.User?.UserID > 0)
            {
                modifierIDProperty.SetValue(item, app?.User?.UserID);
            }

            var deletedProperty = type.GetProperty("IsDeleted");
            if (deletedProperty != null)
            {
                deletedProperty.SetValue(item, false);
            }

            // string类型默认设置为空
            foreach (var p in type.GetProperties())
            {
                if (p.PropertyType.Name == "String")//字符串属性  
                {
                    p.SetValue(item, string.Empty, null);
                }
            }

            if (attachToDbSet)
            {
                dbSet.Add(item);
            }

            return item;
        }

        #endregion

        #region 删除
              

        public int DeletePhysical<T>(int id)
        {
            var type = typeof(T);
            var sql = string.Format("delete from {0} where {0}ID = @p0", type.Name);
            return Database.ExecuteSqlCommand(sql, id);
        }

        public int DeletePhysical<T>(string condition)
        {
            var type = typeof(T);
            var sql = string.Format("delete from {0} where {1}", type.Name, condition);
            return Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 逻辑删除。会将IsDeleted设置为true，更新UpdateTime和ModifierID，不会调用SaveChanges。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void DeleteLogical<T>(int id) where T : class
        {
            var item = Find<T>(id);
            DeleteLogical(item);
        }

        /// <summary>
        /// 逻辑删除。会将IsDeleted设置为true，更新UpdateTime和ModifierID，不会调用SaveChanges。
        /// </summary>
        /// <param name="item"></param>
        public void DeleteLogical(object item)
        {
            var type = item.GetType();

            var deletedProperty = type.GetProperty("IsDeleted");
            if (deletedProperty == null)
            {
                Remove(item);
                return;
            }

            deletedProperty.SetValue(item, true);

            var modifiedTimeProperty = type.GetProperty("UpdateTime");
            if (modifiedTimeProperty != null)
            {
                modifiedTimeProperty.SetValue(item, DateTime.Now);
            }

            var modifierIDProperty = type.GetProperty("UpdateUserID");
            if (modifierIDProperty != null && app?.User?.UserID > 0)
            {
                modifierIDProperty.SetValue(item, app?.User?.UserID);
            }
        }

        #endregion
        

        /// <summary>
        /// 反射得到实体类的字段名称和值
        /// var dict = GetProperties(model);
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实例化</param>
        /// <returns></returns>
        private static Dictionary<object, object> GetProperties<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取指定实体已填充字段数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int GetFieldFinishNum<T>(T t, string[] fields)
        {
            var ret = new Dictionary<object, object>();
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return 0; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);
                }
            }

            var n = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                var a = fields[i];
                var value = ret[a];
                if (value != null && !string.IsNullOrEmpty(value.ToString()) && Convert.ToString(value) != "0")
                {
                    n++;
                }
            }
            return n;
        }


        #region 分页

        public List<T> ToPagedList<T>(IQueryable<T> pageQuery, dynamic condition,IAppContext appContext, out int totalItemCount)
        {
            totalItemCount = 0;
            StackFrame fr = new StackFrame(1, true);
            MethodBase mb = fr.GetMethod();
            var typ = mb.DeclaringType;
            var methodName = mb.Name;
            var path = appContext?.HttpContext?.Request?.Path.Value;
            var isApiRequest = false;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    methodName = appContext?.HttpContext?.Request?.Path.Value.Split('/')[2];
                    isApiRequest = true;
                }
                catch (Exception ex) { Lib.Log.WriteExceptionLog($"ToPagedList Exception:{path}:{path}"); }
            }
            else
            {
                methodName = mb.Name;
            }

            var flag = ((string)JsonExtensions.SerializeObject(condition,new string[] { "PageIndex", "PageSize" })).ToMd5();
            var countCacheKey = CacheManager.Key(typ.Name, methodName, flag + "_Count");                        
            List<T> list = null;

            // api请求才需要异步更新count，单元测试或者异步更新时无需再次出发
            if (!isApiRequest && !string.IsNullOrEmpty(appContext.ReuqestID))// 异步更新总记录数，单元测试无需更新
            {
                var count2 = pageQuery.Count();
                totalItemCount = count2;
                CacheManager.Set(countCacheKey, count2, TimeSpan.FromDays(7), false);
                return list; 
            }

            int pageIndex = condition.PageIndex;
            int pageSize = condition.PageSize;
            var isNeedUpdate = false;
            var countObj = CacheManager.Get<int?>(countCacheKey);            
            if (countObj == null)
            {
                var countTemp = pageSize * 4 + 1;
                var pageQueryTemp = pageQuery.Skip(0).Take(countTemp);
                list = pageQueryTemp.ToList();

                if (list.Count < countTemp)
                {
                    totalItemCount = list.Count;
                }
                else
                {
                    totalItemCount = countTemp;                    
                }
                
                CacheManager.Set(countCacheKey, totalItemCount, TimeSpan.FromMinutes(3));   // 虚拟页数缓存时间缩短
                list = list.Take(pageSize).ToList();
            }
            else
            {
                var itemIndex = (pageIndex - 1) * pageSize;
                list = pageQuery.Skip(itemIndex).Take(pageSize).ToList();
                totalItemCount = Convert.ToInt32(countObj);                
            }

            if (isApiRequest)
            {
                isNeedUpdate = CacheManager.CheckUpdateAndFlag(countCacheKey, CacheTimeSettings.Week);
                if (totalItemCount < list.Count)
                {
                    // 存在缓存的count较小，但实际数据已经更新的情况，强制更新
                    totalItemCount = list.Count;
                    isNeedUpdate = true;
                }

                if (isNeedUpdate)
                {
                    Task.Run(async () =>
                    {
                        var newAppContext = AppManager.CopyAppContext(appContext);
                        Task.Delay(1);
                        CacheManager.Invoke(typ, methodName, newAppContext.ActionArguments, newAppContext);
                    });
                }
            }

            return list;
        }

       
        #endregion
    }
}
