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
    public partial class Form2 : Form
    {
        public TestClass1 testClass1;
        public delegate void ErrorCallBack(Exception e, string errorMsg, DateTime time);
        public event ErrorCallBack LockError;

        public Form2(TestClass1 tc1)
        {
            InitializeComponent();
            testClass1 = tc1;
        }
     

        private void btnConvert2_Click(object sender, EventArgs e)
        {
            testClass1.Test();
        }
    }
}
