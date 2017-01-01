using ClassLibrary1;
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
            for (int i = 0; i < 100; i++)
            {
                if (i + 1 % 3 != 0)
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isSend = Send.IsSendData();
            if (isSend)
            {
                MessageBox.Show(isSend.ToString());
            }
            else
            {
                MessageBox.Show("isSend.ToString()");
            }
        }
    }
}
