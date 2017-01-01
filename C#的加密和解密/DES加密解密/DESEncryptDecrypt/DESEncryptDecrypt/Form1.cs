using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESEncryptDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string rKey = "EMMF6KD6W3";
        string rValue = "E3BV88BWQULQCCTMX29XYELJFRBOMC5L";//每个商户特有的

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDESEncrypt_Click(object sender, EventArgs e)
        {
            var key = rKey.Substring(0, 8);
            var iv = rValue.Substring(rKey.Length - 8, 8);//密钥和向量必须为8位，否则加密解密都不成功。
            string source = this.txtDESEncrypt.Text;

            this.txtDESEncrypt.Text = DESEncrypt(source, key, iv);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDESDecrypt_Click(object sender, EventArgs e)
        {
            var key = rKey.Substring(0, 8);
            var iv = rValue.Substring(rKey.Length - 8, 8);//密钥和向量必须为8位，否则加密解密都不成功。
            string encryptedString = this.txtDESDecrypt.Text;

            this.txtDESDecrypt.Text = DESDecrpt(encryptedString, key, iv);  
        }
       
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string DESEncrypt(string sourceString, string key, string iv)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(iv);
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return sourceString;
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string DESDecrpt(string encryptedString, string key, string iv)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();  
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(iv);
            using (MemoryStream ms = new MemoryStream())  
            {  
                byte[] inData = Convert.FromBase64String(encryptedString);  
                try  
                {  
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))  
                    {  
                        cs.Write(inData, 0, inData.Length);  
                        cs.FlushFinalBlock();  
                    }  
  
                    return Encoding.UTF8.GetString(ms.ToArray());  
                }  
                catch  
                {  
                    return encryptedString;  
                }  
            }  
        } 
    }
}
