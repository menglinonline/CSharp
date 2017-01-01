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
using System.Web.Security;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sd = new StringBuilder();
            sd.Append("aft1=3&CurrentBalance=5&WatchWord=123456&mark=user_rxmny");
            //商户密钥加密 两个商户秘钥    
            var key1 = "7O9HC1V2YE";
            var key2 = "Y28QNG7V9RV8T3M2VN1BJ62UH1UVALKS";
            var Tag = CreateTag(sd.ToString(), key1, key2);
            //商户第二个密钥加密
            var Mdf = CreateMdf(sd.ToString(), key2);
        }

        public static string CreateTag(string str, string k1, string k2)
        {
            var ka = k1.Substring(0, 8);
            var kb = k2.Substring(k2.Length - 8, 8);
            return Encrypt(str, ka, kb);
        }
        public static string Encrypt(string str, string k1, string k2)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.ASCII.GetBytes(k1);
                provider.IV = Encoding.ASCII.GetBytes(k2);
                byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                StringBuilder builder = new StringBuilder();
                foreach (byte num in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", num);
                }
                stream.Close();
                return builder.ToString();

            }
            catch (Exception) { return null; }
        }

        public static string CreateMdf(string str, string k2)
        {
            return DefaultEncodeMD5(str + k2);//md5加密
        }

        public static string DefaultEncodeMD5(string s)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
        }
    }
}
