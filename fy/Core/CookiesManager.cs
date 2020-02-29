//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modobay
{
    // 位于core无需noncontroller [Modobay.Api.NonController]
    public class CookiesManager : ICookiesManager
    {
        private readonly HttpContext _httpContext;
        private readonly IConfiguration configuration;

        public CookiesManager(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            _httpContext = accessor.HttpContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        public void SetCookies(string key, string value, int minutes = 0)
        {
            if (_httpContext == null) return;
            if (_httpContext.Request.Headers["Referer"].Count == 0) return;
            var referer = _httpContext.Request.Headers["Referer"][0];
            var reg1 = new Regex(@"(http|https)://(www.)?(\w+(\.)?)+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            var reg2 = new Regex(@"(http|https)://(www.)?", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            var url = reg1.Match(referer).Groups[0].Value;
            string domain = reg2.Replace(url, "");
            var reg3 = new Regex(@"((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))");
            if (!reg3.IsMatch(domain))
            {
                var domainArray = domain.Split('.');
                if (domainArray.Length == 1)
                {
                    domain = domainArray[0];
                }
                else if (domainArray.Length >= 2)
                {
                    domain = $".{domainArray[domainArray.Length - 2]}.{domainArray[domainArray.Length - 1]}";
                }
            }
            if (!string.IsNullOrEmpty(configuration["CookieDomain"]))
            {
                domain = configuration["CookieDomain"];
            }

            var option = new CookieOptions
            {
                Domain = domain
                //Secure = false,
                //SameSite = SameSiteMode.Lax,
                //Expires = DateTime.Now.AddMinutes(minutes)
            };

            if (minutes > 0)
            {
                option.Expires = DateTime.Now.AddMinutes(minutes);
            }

            //_httpContext.Response.Cookies.Append(key, value, option);

            _httpContext.Response.OnStarting(() =>
            {
                _httpContext.Response.Cookies.Append(key, value, option);
                return Task.FromResult(0);
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        public void DeleteCookies(string key)
        {
            if (_httpContext == null) return;
            _httpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public string GetCookies(string key)
        {
            if (_httpContext == null) return string.Empty;
            string value = string.Empty;
            try
            {
                value = _httpContext.Request.Cookies[key];
            }
            catch (Exception ex)
            {
            }
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
