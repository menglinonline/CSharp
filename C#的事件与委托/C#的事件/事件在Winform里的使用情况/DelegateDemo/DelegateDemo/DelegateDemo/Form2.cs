using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DelegateDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //public delegate void EventHandler(object sender, EventArgs e);
        //public event EventHandler Click;
        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Click += new EventHandler(Msg);
        }


        private void Msg(object sender, EventArgs e)
        {
            MessageBox.Show("xxx");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
