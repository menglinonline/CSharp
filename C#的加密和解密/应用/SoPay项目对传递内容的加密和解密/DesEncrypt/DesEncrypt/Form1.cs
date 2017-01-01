using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesEncrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string md5key = "8fe789a8acb0465db0560faa91056c15";
            string deskey1 = "4NWvavdEHNE=";
            string deskey2 = "EhQ/mTMPMR4=";

            XMLEntity xmlentity = new XMLEntity();
            xmlentity.CMD = "6006";
            xmlentity.MERCHANTID = "ML9860";
            xmlentity.ORDER = "636074602560312500";
            xmlentity.USERNAME = "ML9860";
            xmlentity.UNIT = "1";
            xmlentity.BACK_URL = "http://211.149.219.209:8026/Default.aspx";
            xmlentity.BACK_URL_BROWSER = "http://211.149.219.209:8020/";
            xmlentity.TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            xmlentity.REMARK = "内容加密";
            xmlentity.MONEY = "100";
            string xml = GetXmlStr(xmlentity);
            //加密
            string md5 = MyEncrypt.MD5Encrypt(xml + md5key);
            string d = xml + md5;
            string des = MyEncrypt.EncryptData(d, deskey1, deskey2);

            //解密
            string value = MyDecrypt.GetDecryptData("ML9860", des, deskey1 + "," + deskey2 + "," + md5key);
        }


        /// <summary>
        /// 得到xml格式字符串
        /// </summary>
        /// <param name="xmlentity"></param>
        /// <returns></returns>
        public static string GetXmlStr(XMLEntity xmlentity)
        {
            string xmlStr = string.Empty;
            try
            {
                xmlStr = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>  "
                + "<message>" + "<cmd>" + xmlentity.CMD + "</cmd>"
                    + "<merchantid>" + xmlentity.MERCHANTID + "</merchantid >"
                    + "<language>" + xmlentity.LANGUAGE + "</language>"
                    + "<userinfo>" +
                        "<order>" + xmlentity.ORDER + "</order>"
                        + "<username>" + xmlentity.USERNAME + "</username>"
                        + "<paychannel>" + xmlentity.PAYCHANNEL + "</paychannel>"
                        + "<bank>" + xmlentity.BANK + "</bank>"
                        + "<money>" + Convert.ToDecimal(xmlentity.MONEY).ToString("0.00") + "</money>"
                        + "<unit>" + xmlentity.UNIT + "</unit>"
                        + "<time>" + xmlentity.TIME + "</time>"
                        + "<remark>" + xmlentity.REMARK + "</remark>"
                        + "<backurl>" + xmlentity.BACK_URL + "</backurl>"
                        + "<backurlbrowser>" + xmlentity.BACK_URL_BROWSER + "</backurlbrowser>"
                    + "</userinfo>"
                + "</message>";
            }
            catch (Exception ex)
            {
            }

            return xmlStr;
        }
    }
}
