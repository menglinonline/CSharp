using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmPattern : Form
    {
        public FrmPattern()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                FrmFolder form2 = new FrmFolder();
                form2.ShowDialog();
            }
            else if(this.radioButton2.Checked)
            {
                FrmFile form1 = new FrmFile();
                form1.ShowDialog();
            }
        }
    }
}
