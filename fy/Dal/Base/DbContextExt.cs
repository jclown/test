using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dal
{
    public static class DbContextExt
    {
        private static LoggerFactory loggerFactory = new LoggerFactory();

        static DbContextExt()
        {
            loggerFactory.AddProvider(new Dal.Log.EFLoggerProvider());            
        }

        public static void AddDbContext3(this IServiceCollection services,params string[] connectionString)
        {
            services.AddDbContext<Dal.MLSDbContext>(options =>
            {
                options.UseSqlServer(connectionString[0]);
                options.UseLoggerFactory(loggerFactory);
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddDbContext<Dal.FyDbContext>(options =>
            {
                options.UseSqlServer(connectionString[1]);
                options.UseLoggerFactory(loggerFactory);
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
            //services.AddDbContextPool<Dal.IDSDbContext>(options =>
            //{
            //    options.UseMySQL(mainConnectionString);
            //    options.UseLoggerFactory(loggerFactory);
            //    options.EnableSensitiveDataLogging();
            //}, 500);
        }
                        
        public static TContext GetDbContext<TContext>() where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseMemoryCache(Modobay.Cache.CacheManager.AppCache);
            optionsBuilder.UseSqlServer(Modobay.ConfigManager.Configuration.GetConnectionString("SqlServer").Replace("{DataServerIP}", Modobay.AppManager.DataServerIP));
            var options = optionsBuilder.Options;
            return (TContext)System.Activator.CreateInstance(typeof(TContext), options);
        }

        private static void CombineParams(ref DbCommand command, params object[] parameters)
        {
            if (parameters != null)
            {
                foreach (MySqlParameter parameter in parameters)
                {
                    if (!parameter.ParameterName.Contains("@"))
                        parameter.ParameterName = $"@{parameter.ParameterName}";
                    command.Parameters.Add(parameter);
                }
            }
        }

        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection dbConn, params object[] parameters)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            if (facade.IsSqlServer())
            {
                cmd.CommandText = sql;
                CombineParams(ref cmd, parameters);
            }
            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, parameters);
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            DataTable dt = SqlQuery(facade, sql, parameters);
            return dt.ToEnumerable<T>();
        }

        public static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T[] ts = new T[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                        p.SetValue(t, row[p.Name], null);
                }
                ts[i] = t;
                i++;
            }
            return ts;
        }

        #region 分页

        /// <summary>
        /// 分页 拓展方法
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="allItems">IQueryable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static PagedList<T> GetPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 30) pageSize = 30;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = allItems.Count();

            // 超出范围默认取最后1页
            //while (totalItemCount <= itemIndex && pageIndex > 1)
            //{
            //    itemIndex = (--pageIndex - 1) * pageSize;
            //}

            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);

            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        /// <summary>
        /// 分页 拓展方法
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="allItems">IQueryable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static PagedList<T> GetPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            return allItems.AsQueryable().GetPagedList(pageIndex, pageSize);
        }


        #endregion
    }
}
