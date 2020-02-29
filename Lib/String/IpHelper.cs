using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.String
{
    public class IpHelper
    {

        /// <summary>
        /// 根据IP获取国家
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static string GetRequestIpCode(string IP)
        {
            try
            {
                if (IP.StartsWith("192.") || IP.StartsWith("127."))
                {
                    return "CN";
                }
                var res = Lib.HttpHelper.PostHttpResponse($"http://ip.taobao.com/service/getIpInfo.php?ip={IP}", "");
                dynamic obj = Newtonsoft.Json.Linq.JObject.Parse(res);
                if (obj!=null&&obj.code == 0)
                {
                    return obj.data.country_id;
                }
            }
            catch (Exception)
            {
            }

            return "";
        }
    }
}
