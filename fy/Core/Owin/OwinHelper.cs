using Owin.Token.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Modobay
{
    public class OwinHelper
    {
        private const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
        private const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static string GetIdentity(string token)
        {
            var encryptMode = Owin.Token.AspNetCore.EncryptionMethod.TripleDES;
            if (ConfigManager.Configuration["OwinMachineKey:decryption"] == "AES")
            {
                encryptMode = Owin.Token.AspNetCore.EncryptionMethod.AES;
            }

            var option = new LegacyTokenAuthenticationOptions
            {
                DecryptionKey = ConfigManager.Configuration["OwinMachineKey:decryptionKey"],
                ValidationKey = ConfigManager.Configuration["OwinMachineKey:validationKey"],
                EncryptionMethod = encryptMode,
                ValidationMethod = ValidationMethod.SHA1 // Default HMACSHA256
            };
            AuthenticationTicket ticket = null;
            try
            {
                ticket = LegacyOAuthSecurityTokenHelper.GetTicket(token, option); ;
            }
            catch (Exception ex)
            {
                throw new AppException(ex, AppExceptionType.AppTokenInvalid);
            }

            if (ticket == null) throw new AppException(AppExceptionType.AppTokenInvalid);
            //var claimsValue = ticket?.Identity?.Claims.FirstOrDefault(x => x.Type == NameIdentifier || x.Type == MobilePhone) ?? throw new Exception();
            var claimsValue = ticket?.Identity?.Claims.FirstOrDefault(x => x.Type == NameIdentifier) ?? throw new Exception();
            if (claimsValue == null || claimsValue.Value == null) throw new AppException(AppExceptionType.AppTokenInvalid);
            var identifier = claimsValue.Value;
            return identifier;
        }
    }
}
