using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Modobay.Api
{
    /// <summary>
    /// 表示http请求方法描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class HttpMethodAttribute : Attribute
    {
        /// <summary>
        /// 获取请求方法
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// 获取请求相对路径
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// http请求方法描述特性
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="path">请求相对路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HttpMethodAttribute(HttpMethod method, string path)
        {
            this.Method = method;
            this.Path = path.TrimEnd('&');
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.Method, this.Path);
        }
    }
}