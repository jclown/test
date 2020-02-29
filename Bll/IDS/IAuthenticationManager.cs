using System;
using System.Collections.Generic;
using System.Net.Http;
using Dto;

namespace Modobay
{
    public interface IAuthenticationManager : IAuthentication
    {        
        UserDto CheckToken(string token);
    }
}