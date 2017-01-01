using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace CreateQRCodeDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = CreateImgCode("weixin://wxpay/bizpayurl?pr=Esw6x14", 8);
            System.Drawing.Image myimg = bitmap;
            MemoryStream ms = new System.IO.MemoryStream(); //实例化内存流对象
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//将绘制好的图像以二进制流的形式存储在内存流中
            Response.ClearContent();//先清除未输出内容
            Response.ContentType = "image/Png"; //定义输出类型
            Response.BinaryWrite(ms.ToArray());//将流显示在页面上
            Response.End();
        }

        public Bitmap CreateImgCode(string codeNumber, int size)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }
    }
}