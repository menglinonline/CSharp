using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateUI);
            thread.Start();
        }

        private void UpdateUI()
        {
            this.label1.Text = "测试";
            this.Invoke((EventHandler)delegate
            {
                this.label1.Text = "测试";
            });
            this.Invoke((EventHandler)delegate { this.label1.Text = "测试"; });
        }
    }
}
