using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAEncryptDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = "张三";
            string userPwd = "123456";
            string[] keys = GenerateKeys();

            string encryptedString = EncryptString(userName, keys[0]);
            string decryptedString = DecryptString(encryptedString, keys[1]);

            string encryptedString2 = EncryptString2();
            string decryptedString2 = DecryptString2(encryptedString2);
        }

        /// <summary>
        /// generate private key and public key arr[0] for private key arr[1] for public key
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

       static CspParameters param;

        public static string EncryptString2()
        {
            string sbString = string.Empty;
            param = new CspParameters();  
            param.KeyContainerName = "Olive";//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes("userName=123456&pwd=sddsksdkjhwiuwekjewkjdds");//将要加密的字符串转换为字节数组  
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组   
                sbString = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串           
            }

            return sbString.ToString();
        }

        public static string DecryptString2(string sSource)
        {
            string sb = string.Empty;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param)) 
            {
                byte[] encryptdata = Convert.FromBase64String(sSource);    
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                sb = Encoding.Default.GetString(decryptdata);            
            }
            return sb;
        }




        /// <summary>
        /// RSA Encrypt
        /// </summary>
       /// <param name="sSource" >Source string</param>
        /// <param name="sPublicKey" >public key</param>
        /// <returns></returns>
        public static string EncryptString(string sSource, string sPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string plaintext = sSource;
            rsa.FromXmlString(sPublicKey);
            byte[] cipherbytes;
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);



            StringBuilder sbString = new StringBuilder();
            for (int i = 0; i < cipherbytes.Length; i++)
            {
                sbString.Append(cipherbytes[i] + ",");
            }

            return sbString.ToString();
        }

        /// <summary>
        /// RSA Decrypt
        /// </summary>
        /// <param name="sSource">Source string</param>
        /// <param name="sPrivateKey">Private Key</param>
        /// <returns></returns>
        public static string DecryptString(String sSource, string sPrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(sPrivateKey);
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
            string[] sBytes = sSource.Split(',');



            for (int j = 0; j < sBytes.Length; j++)
            {
                if (sBytes[j] != "")
                {
                    byteEn[j] = Byte.Parse(sBytes[j]);
                }
            }
            byte[] plaintbytes = rsa.Decrypt(byteEn, false);
            return Encoding.UTF8.GetString(plaintbytes);
        }
    }
}
