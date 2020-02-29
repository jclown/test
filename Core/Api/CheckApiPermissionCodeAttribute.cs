using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckApiPermissionCodeAttribute : Attribute
    {
        /// <summary>
        /// 完整命名为三级，服务端设置二级名称即可检查所有应用的权限。完整命名规范为“AppID:模块名称:功能点名称”，示例Supplier:ProductManager:View。
        /// </summary>
        public string PermissionCode { get; set; }
    }
}