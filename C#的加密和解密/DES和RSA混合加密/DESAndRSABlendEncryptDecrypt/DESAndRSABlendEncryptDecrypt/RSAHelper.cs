using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DESAndRSABlendEncryptDecrypt
{
    public class RSAHelper
    {
        /// <summary>
        /// 非对称加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="publicKeyXml">公钥的xml</param>
        /// <returns>密文的base64字符串</returns>
        public static string RASEncrypt(string data, string publicKeyXml)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyXml);
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            string resultBase64 = Convert.ToBase64String(rsa.Encrypt(databytes, false));

            return resultBase64;
        }

        /// <summary>
        /// 非对称解密
        /// </summary>
        /// <param name="data">密文的base64字符串</param>
        /// <param name="privateKeyXml">私钥的xml</param>
        /// <returns>明文UFT8字符串</returns>
        public static string RSADecrypt(string data, string privateKeyXml)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXml);
            byte[] databytes = Convert.FromBase64String(data);
            string resultUTF8 = Encoding.UTF8.GetString(rsa.Decrypt(databytes, false));
            return resultUTF8;
        }

        /// <summary>
        /// 用非对称私钥签名
        /// </summary>
        /// <param name="privateKeyXml">私钥的xml</param>
        /// <param name="data">明文</param>
        /// <returns>签名的base64字符串</returns>
        public static string SignData(string data, string privateKeyXml)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXml);
            byte[] signData = rsa.SignData(Encoding.UTF8.GetBytes(data), CryptoConfig.MapNameToOID("SHA1"));
            string sign64 = Convert.ToBase64String(signData);
            return sign64;
        }

        /// <summary>
        /// 用非对称公钥与明文验证签名
        /// </summary>     
        /// <param name="data"></param>
        /// <param name="publicKeyXml"></param>
        /// <param name="signData"></param>
        /// <returns></returns>
        public static bool SignVerifyData(string data, string publicKeyXml, string signData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyXml);
            byte[] signDataBytes = Convert.FromBase64String(signData);
            bool isV = rsa.VerifyData(Encoding.UTF8.GetBytes(data), CryptoConfig.MapNameToOID("SHA1"), signDataBytes);
            return isV;
        }
    }
}
