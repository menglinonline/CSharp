using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesEncrypt
{
    public class MyDecrypt
    {
        public string Key1;
        public string Key2;

        /// <summary>
        /// 得到解密后的数据
        /// </summary>
        /// <param name="pid">商户号</param>
        /// <param name="encryptedData">加密后的数据</param>
        /// <param name="merchantKey">商户key</param>
        /// <returns></returns>
        public static string GetDecryptData(string pid, string encryptedData, string merchantKey)
        {
            string xmlValue = string.Empty;
            try
            {
                //根据商户号,获取对应的加密密钥
                //string merchantKey = GetkeyBypid(pid);
                //解密之后的XML字符
                string desXmlValue = string.Empty;
                string[] ar = merchantKey.Split(',');
                string xml = "";
                //deskey1: 商户的DES密钥key(加密)
                //deskey2: 商户的DES密钥IV(加密)
                //md5key : 商户MD5密钥
                string deskey1 = string.Empty;
                string deskey2 = string.Empty;
                string md5key = string.Empty;
                if (ar.Length == 3)
                {
                    deskey1 = !string.IsNullOrEmpty(ar[0]) ? ar[0] : "";
                    deskey2 = !string.IsNullOrEmpty(ar[1]) ? ar[1] : "";
                    md5key = !string.IsNullOrEmpty(ar[2]) ? ar[2] : "";
                    desXmlValue = DecryptData(encryptedData, deskey1, deskey2);

                    string md5xml = string.Empty;
                    string md5new = string.Empty;
                    if (!string.IsNullOrEmpty(desXmlValue))
                    {
                        xml = desXmlValue.Substring(0, desXmlValue.Length - 32);
                        md5xml = desXmlValue.Substring(desXmlValue.Length - 32, 32);
                        md5new = MyEncrypt.MD5Encrypt(xml + md5key);
                    }

                    //md5验证
                    if (!string.IsNullOrEmpty(md5new) && !string.IsNullOrEmpty(md5xml) && md5new.Equals(md5xml))
                    {
                        xmlValue = xml;
                    }
                    else
                    {
                        xmlValue = "商户秘钥有误或信息被篡改!";
                    }
                }
                else
                {
                    xmlValue = "商户秘钥有误，请联系管理员!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return xmlValue;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">加密后的字符串</param>
        /// <param name="key1">第一个key</param>
        /// <param name="key2">第二个key</param>
        /// <returns></returns>
        public static string DecryptData(string data, string key1, string key2)
        {
            MyDecrypt Decrypt = new MyDecrypt();
            Decrypt.Key1 = key1;
            Decrypt.Key2 = key2;
            string ret = string.Empty;
            try
            {
                ret = Decrypt.DecryptData(data);
                ret = ret.Substring(0, ret.Length - 32);
                return ret;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 解密加密后的数据
        /// </summary>
        /// <param name="data">加密后的字符串</param>
        /// <returns></returns>
        private string DecryptData(string data)
        {
            byte[] keyBytes = Convert.FromBase64String(Key1);
            byte[] keyIV = Convert.FromBase64String(Key2);
            byte[] inputByteArray = Convert.FromBase64String(data);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();

            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
