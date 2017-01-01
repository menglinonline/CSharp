using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string str = "我爱你哦！";           
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            //转成 Base64 形式的 System.String
            string base64String = Convert.ToBase64String(bytes);

            //转回到原来的 System.String。
            byte[] bytes2 = Convert.FromBase64String(base64String);
            string strPlan = Encoding.UTF8.GetString(bytes2);
        }
    }
}
