using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiClient.ES
{
    public static class ESHelper
    {
        /// <summary>
        /// 获取ElasticClient
        /// </summary>
        /// <param name="url">ElasticSearch服务器地址</param>
        /// <param name="defaultIndex">默认索引名称</param>
        /// <param name="defaultType">默认类型</param>
        /// <returns></returns>
        public static ElasticClient Client(string url, string defaultIndex = "", string defaultType = "")
        {
            var uri = new Uri(url);
            var setting = new ConnectionSettings(uri);

            if (!string.IsNullOrWhiteSpace(defaultIndex))
            {
                setting.DefaultIndex(defaultIndex);
            }

            if (!string.IsNullOrWhiteSpace(defaultType))
            {
                // todo pxg 暂时屏蔽
                //setting.DefaultTypeName(defaultType);
            }

            return new ElasticClient(setting);
        }

        /// <summary>
        /// 获取ElasticClient
        /// </summary>
        /// <param name="urls">ElasticSearch集群地址</param>
        /// <param name="defaultIndex">默认索引名称</param>
        /// <param name="defaultType">默认类型</param>
        /// <returns></returns>
        public static ElasticClient Client(string[] urls, string defaultIndex = "", string defaultType = "")
        {
            var uris = urls.Select(h => new Uri(h)).ToArray();
            var pool = new SniffingConnectionPool(uris);

            var setting = new ConnectionSettings(pool);

            if (!string.IsNullOrWhiteSpace(defaultIndex))
            {
                setting.DefaultIndex(defaultIndex);
            }

            if (!string.IsNullOrWhiteSpace(defaultType))
            {
                // todo pxg 暂时屏蔽
                //setting.DefaultTypeName(defaultType);
            }

            return new ElasticClient(setting);
        }

        /// <summary>
        /// 如果同名索引不存在则创建索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">ElasticClient实例</param>
        /// <param name="indexName">要创建的索引名称</param>
        /// <param name="numberOfReplicas">默认副本数量，如果是单实例，注意改成0</param>
        /// <param name="numberOfShards">默认分片数量</param>
        /// <returns></returns>
        public static bool CreateIndex<T>(this ElasticClient client, string indexName = "", int numberOfReplicas = 1, int numberOfShards = 5) where T : class
        {
            // todo pxg 暂时屏蔽
            //if (client.IndexExists(indexName).Exists) return false;

            var indexState = new IndexState
            {
                Settings = new IndexSettings
                {
                    NumberOfReplicas = numberOfReplicas, //副本数
                    NumberOfShards = numberOfShards //分片数
                }
            };

            if (string.IsNullOrWhiteSpace(indexName))
            {
                indexName = typeof(T).Name.ToLower();
            }

            // todo pxg 暂时屏蔽
            //var result = client.CreateIndex(indexName, c => c.InitializeUsing(indexState).Mappings(ms => ms.Map<T>(m => m.AutoMap())));
            //return result.Acknowledged;
            return false;
        }

        /// <summary>
        /// 返回一个正序排列的委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public static Func<SortDescriptor<T>, SortDescriptor<T>> Sort<T>(string field) where T : class
        {
            return sd => sd.Ascending(field);
        }

        public static Func<SortDescriptor<T>, SortDescriptor<T>> Sort<T>(Expression<Func<T, object>> field) where T : class
        {
            return sd => sd.Ascending(field);
        }

        /// <summary>
        /// 返回一个倒序排列的委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public static Func<SortDescriptor<T>, SortDescriptor<T>> SortDesc<T>(string field) where T : class
        {
            return sd => sd.Descending(field);
        }

        public static Func<SortDescriptor<T>, SortDescriptor<T>> SortDesc<T>(Expression<Func<T, object>> field) where T : class
        {
            return sd => sd.Descending(field);
        }

        /// <summary>
        /// 返回一个Must条件集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<Func<QueryContainerDescriptor<T>, QueryContainer>> Must<T>() where T : class
        {
            return new List<Func<QueryContainerDescriptor<T>, QueryContainer>>();
        }

        public static List<Func<QueryContainerDescriptor<T>, QueryContainer>> Should<T>() where T : class
        {
            return new List<Func<QueryContainerDescriptor<T>, QueryContainer>>();
        }

        /// <summary>
        /// 添加Match子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要查询的关键字</param>
        /// <param name="boost"></param>
        public static void AddMatch<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            string value, double? boost = null) where T : class
        {
            musts.Add(d => d.Match(mq => mq.Field(field).Query(value).Boost(boost)));
        }

        public static void AddMatch<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, string value) where T : class
        {
            musts.Add(d => d.Match(mq => mq.Field(field).Query(value)));
        }

        /// <summary>
        /// 添加MultiMatch子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="fields">要查询的列</param>
        /// <param name="value">要查询的关键字</param>
        public static void AddMultiMatch<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            string[] fields, string value) where T : class
        {
            musts.Add(d => d.MultiMatch(mq => mq.Fields(fields).Query(value)));
        }

        /// <summary>
        /// 添加MultiMatch子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="fields">例如：f=>new [] {f.xxx, f.xxx}</param>
        /// <param name="value">要查询的关键字</param>
        public static void AddMultiMatch<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> fields, string value) where T : class
        {
            musts.Add(d => d.MultiMatch(mq => mq.Fields(fields).Query(value)));
        }

        /// <summary>
        /// 添加大于子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要比较的值</param>
        public static void AddGreaterThan<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).GreaterThan(value)));
        }

        public static void AddGreaterThan<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).GreaterThan(value)));
        }

        /// <summary>
        /// 添加大于等于子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要比较的值</param>
        public static void AddGreaterThanEqual<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).GreaterThanOrEquals(value)));
        }

        public static void AddGreaterThanEqual<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).GreaterThanOrEquals(value)));
        }

        /// <summary>
        /// 添加小于子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要比较的值</param>
        public static void AddLessThan<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).LessThan(value)));
        }

        public static void AddLessThan<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).LessThan(value)));
        }

        /// <summary>
        /// 添加小于等于子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要比较的值</param>
        public static void AddLessThanEqual<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).LessThanOrEquals(value)));
        }

        public static void AddLessThanEqual<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, double value) where T : class
        {
            musts.Add(d => d.Range(mq => mq.Field(field).LessThanOrEquals(value)));
        }

        /// <summary>
        /// 添加一个Term，一个列一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field">要查询的列</param>
        /// <param name="value">要比较的值</param>
        public static void AddTerm<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            object value) where T : class
        {
            musts.Add(d => d.Term(field, value));
        }

        public static void AddTerm<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, object value) where T : class
        {
            musts.Add(d => d.Term(field, value));
        }

        /// <summary>
        /// 添加一个Terms，一个列多个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="musts"></param>
        /// <param name="field"></param>
        /// <param name="values"></param>
        public static void AddTerms<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts, string field,
            object[] values) where T : class
        {
            musts.Add(d => d.Terms(tq => tq.Field(field).Terms(values)));
        }

        public static void AddTerms<T>(this List<Func<QueryContainerDescriptor<T>, QueryContainer>> musts,
            Expression<Func<T, object>> field, object[] values) where T : class
        {
            musts.Add(d => d.Terms(tq => tq.Field(field).Terms(values)));
        }
    }
}
