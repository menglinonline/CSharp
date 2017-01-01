using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESAndRSABlendEncryptDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 商户加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //（公钥和私钥一定要配对，这个Demo中两对公钥和私钥都已经是配过对，配对使用小工具）
            //我们给商户的RSA公钥
            string publicKey = this.txtAPIPublicKey.Text;
            //商户自己的私钥
            string privateKey = this.txtSalePrivateKey.Text;
            //要加密的的内容(报文明文)
            string content = this.txtContent.Text;
            //商户自己后台的DES key
            string desKey = this.txtDESKey.Text;
            //商户自己后台的DES value
            string desValue = this.txtDESValue.Text;
            desKey = desKey.Substring(0, 8);
            desValue = desValue.Substring(desValue.Length - 8, 8);//密钥和向量必须为8位，否则加密解密都不成功。

            string strDesKey = "desKey=" + desKey + "&desValue=" + desValue;
            //用我们给商户的RSA公钥加密“DES key”得到“加密后的DES key”
            string encryptDESKey = RSAHelper.RASEncrypt(strDesKey, publicKey);
            string w = RSAHelper.RSADecrypt(encryptDESKey, this.txtAPIPrivateKey.Text);

            string encryptDESKey2 = RSAHelper.RASEncrypt(strDesKey, this.txtSalePublicKey.Text);
            string w2 = RSAHelper.RSADecrypt(encryptDESKey2, this.txtSalePrivateKey.Text);

          

            //用DES key和DES value加密“报文明文”得到“加密后的报文”
            string encryptContent = DESHelper.DESEncrypt(content, desKey, desValue);

            //用商户自己的RSA私钥加密“报文明文”得到签名
            string sign = RSAHelper.SignData(content, privateKey);

            this.txtEncryptDESKey.Text = encryptDESKey;
            this.txtEncryptContent.Text = encryptContent;
            this.txtSign.Text = sign;
        }

        /// <summary>
        /// API端解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDencrypt_Click(object sender, EventArgs e)
        {
            //商户给我们的公钥
            string publicKey = this.txtSalePublicKey.Text;
            //API开发人员自己的私钥
            string privateKey = this.txtAPIPrivateKey.Text;

            //1.用API开发人员自己的私钥解密“发送过来加密后的DES key”得到明文DES key
            string strDesKey = RSAHelper.RSADecrypt(this.txtEncryptDESKey.Text, privateKey);
            Dictionary<string, string> dic = TransformHelper.ConvertToDictionary(strDesKey);

            //2.这里只有完成了第1步才能完成这步(要想用des解密，首先得通过第1步把加密的des解密出来)
            //用解密后的DES key解密“发送过来的内容(加密报文)”得到报文明文
            string content = DESHelper.DESDecrpt(this.txtEncryptContent.Text, dic["desKey"], dic["desValue"]);

            //用商户给我们的RSA公钥进行验签
            bool isSuccess = RSAHelper.SignVerifyData(content, publicKey, this.txtSign.Text);
            if (isSuccess)
            {
                this.txtContent2.Text = content;
                MessageBox.Show("验签成功");
            }
        }
    }
}
