using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lib.EncryptDES
{
    public static class SignatureHelper
    {
     
        #region 获取签名
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgEncrypt"></param>
        /// <param name="sMsgSignature"></param>
        /// <returns></returns>
        public static int GenarateSinatureSHA1(string sTimeStamp, string sNonce, string sMsgEncrypt, string sToken, ref string sMsgSignature)
        {
            ArrayList AL = new ArrayList();
            AL.Add(sToken);
            AL.Add(sTimeStamp);
            AL.Add(sNonce);
            AL.Add(sMsgEncrypt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }
            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return (int)BizMsgCryptErrorCode.BizMsgCrypt_ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return 0;
        }

        

        #endregion

        #region 验证签名
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsg"></param>
        /// <param name="sSigture"></param>
        /// <returns></returns>
        public static int VerifySignatureSHA1(string sTimeStamp, string sNonce, string sMsg, string sToken, string sSigture)
        {
            string hash = "";
            int ret = 0;
            ret = GenarateSinatureSHA1(sTimeStamp, sNonce, sMsg, sToken, ref hash);
            if (ret != 0)
                return ret;
            if (hash == sSigture)
                return 0;
            else
            {
                return (int)BizMsgCryptErrorCode.BizMsgCrypt_ValidateSignature_Error;
            }
        }

       
        #endregion

        #region 获取16位数随机码Nonce
        /// <summary>
        /// 获取16位数随机码Nonce
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        public static string CreateNonce(int codeLen = 16)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
        #endregion

        #region 获取TimeStamp
        /// <summary>
        /// 获取TimeStamp
        /// </summary>
        /// <returns></returns>
        public static string CreateTimeStamp()
        {
            return DateTime.Now.ConvertToTimeStampLocal().ToString();
        }


        public static long CreateTimeStampLong()
        {
            return DateTime.Now.ConvertToTimeStampLocal();
        }

        #endregion

    }

    public class DictionarySort : System.Collections.IComparer
    {
        public int Compare(object oLeft, object oRight)
        {
            string sLeft = oLeft as string;
            string sRight = oRight as string;
            int iLeftLength = sLeft.Length;
            int iRightLength = sRight.Length;
            int index = 0;
            while (index < iLeftLength && index < iRightLength)
            {
                if (sLeft[index] < sRight[index])
                    return -1;
                else if (sLeft[index] > sRight[index])
                    return 1;
                else
                    index++;
            }
            return iLeftLength - iRightLength;

        }
    }

    enum BizMsgCryptErrorCode
    {
        BizMsgCrypt_OK = 0,
        BizMsgCrypt_ValidateSignature_Error = -40001,
        BizMsgCrypt_ComputeSignature_Error = -40003,
        BizMsgCrypt_IllegalAesKey = -40004,
        BizMsgCrypt_ValidateAppid_Error = -40005,
        BizMsgCrypt_EncryptAES_Error = -40006,
        BizMsgCrypt_DecryptAES_Error = -40007,
        BizMsgCrypt_IllegalBuffer = -40008,
        BizMsgCrypt_EncodeBase64_Error = -40009,
        BizMsgCrypt_DecodeBase64_Error = -40010
    };
}
