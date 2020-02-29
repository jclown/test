
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Modobay
{
    public class AppContextMini
    {
        public int UserID { get; set; } = -1;
        public int CorpID { get; set; } = -1;
        public string AppID { get; set; }
        public string Language { get; set; }
        public string ReuqestID { get; set; }
        public string TaskId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public IDictionary<string, object> ActionArguments { get; set; } = new Dictionary<string, object>();
    }
    public class UserDto
    {
        public UserDto()
        {
            //this.Permissions = new List<string>();
            //this.Roles = new List<string>();
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        //public string Token { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        //public int CorpID { get; set; }
        public int ShopId { get; set; }

        public int DepartmentId { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        //public List<string> Permissions { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        //public List<string> Roles { get; set; }
    }
}

