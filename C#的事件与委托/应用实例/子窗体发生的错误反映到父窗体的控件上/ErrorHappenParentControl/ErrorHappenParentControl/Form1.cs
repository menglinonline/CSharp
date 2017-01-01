using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestClass1 testclass1 = new TestClass1();
            testclass1.OnError += testclass1_OnError;

            var form2 = new Form2(testclass1);
            form2.ShowDialog();
        }

        void testclass1_OnError(Exception e, string errorMsg, DateTime time)
        {
            string msg = string.Format("错误原因：{0}，错误描述：{1}，错误时间：{2}", e.ToString(), errorMsg, time.ToString());
            richTextBoxMsg.AppendText(msg);
        }
    }
}
