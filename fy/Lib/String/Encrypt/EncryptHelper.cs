using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lib
{
    public static class EncryptHelper
    {
        public static string GetSHA1(string str)
        {
            var sb = new StringBuilder();
            foreach (var b in SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(str)))
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        #region DES加密解密

        public static string EncryptDES(string content, string key)
        {
            return new DES().EncryptDES(content, key);
        }

        public static string DecryptDES(string content, string key)
        {
            return new DES().DecryptDES(content, key);
        }

        #endregion


        #region AES加密解密

        public static string EncryptAES(string content, string key)
        {
            return new AES(key).EncryptAES(content, key);
        }

        public static string DecryptAES(string content, string key)
        {
            return new AES(key).DecryptAES(content, key);
        }

        #endregion
    }
}
