using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay
{
    public interface IApiPermission
    {
        void CheckAppApiPermission(string api);
        void CheckUserApiPermission(string api);
    }
}
