using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesEncrypt
{
    public class MyEncrypt
    {
        public string Key1;
        public string Key2;

        public void getKeys()
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.GenerateIV();
            provider.GenerateKey();
            Key1 = Convert.ToBase64String(provider.Key);
            Key2 = Convert.ToBase64String(provider.IV);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密的字符串</param>
        /// <param name="key1">第一个key</param>
        /// <param name="key2">第二个key</param>
        /// <returns></returns>
        public static string EncryptData(string data, string key1, string key2)
        {
            MyEncrypt encrypt = new MyEncrypt();
            encrypt.Key1 = key1;
            encrypt.Key2 = key2;
            string unEncryptData = data + MD5Encrypt(GetMac() + DateTime.Now.ToFileTime());

            return encrypt.EncryptData(unEncryptData);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密的字符串</param>
        /// <returns></returns>
        public string EncryptData(string data)
        {
            byte[] keyBytes = Convert.FromBase64String(Key1);//ASCIIEncoding.ASCII.GetBytes(key);
            byte[] keyIV = Convert.FromBase64String(Key2);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(data);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static string GetMac()
        {
            string mac = "";
            Random r = new Random();
            mac = System.DateTime.Now.ToFileTime().ToString() + r.Next().ToString();
            //string hostInfo = Dns.GetHostName();
            ////System.Net.IPAddress[] addressList = Dns.GetHostEntry("hostInfo");// Dns.GetHostByName(Dns.GetHostName()).AddressList;
            //System.Net.IPHostEntry Entry = Dns.GetHostEntry(hostInfo);
            //System.Net.IPAddress[] addressList = Entry.AddressList;
            //for (int i = 0; i < addressList.Length; i++)
            //{

            //    s += addressList[i].ToString();
            //}
            //ManagementClass mc;

            //mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection moc = mc.GetInstances();
            //foreach (ManagementObject mo in moc)
            //{

            //    if (mo["IPEnabled"].ToString() == "True")

            //        mac += mo["MacAddress"].ToString();
            //}
            return mac;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptData(string data, string key)
        {
            //pToEncrypt = HttpContext.Current.Server.UrlEncode(pToEncrypt);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(data);
            //建立加密对象的密钥和偏移量
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

      
    }
}
