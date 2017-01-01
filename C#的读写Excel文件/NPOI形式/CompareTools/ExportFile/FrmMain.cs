using CompareTools;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void smiDuiZhangDan_Click(object sender, EventArgs e)
        {
            FrmDuiZhangDan form = new FrmDuiZhangDan();
            form.ShowDialog();
        }

        private void smiDuiZhangDanRiRiJieSuiXinFu_Click(object sender, EventArgs e)
        {
            FrmAll form = new FrmAll();
            form.ShowDialog();
        }


        private void rD文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRD form = new FrmRD();
            form.ShowDialog();
        }

        private void 系统订单和银行订单对账ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountChecking.FrmSingleCheck form = new AccountChecking.FrmSingleCheck();
            form.ShowDialog();
        }

        private void 文件抽取商户号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountChecking.FrmSingleConvert form = new AccountChecking.FrmSingleConvert();
            form.ShowDialog();
        }

        private void 通联信用卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTLXYK form = new FrmTLXYK();
            form.ShowDialog();
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTrader_Click(object sender, EventArgs e)
        {
            FrmAddTrader form = new FrmAddTrader();
            form.ShowDialog();
        }

        private void rD文件导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRD form = new FrmRD();
            form.ShowDialog();
        }

        private void rD信用卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRDXYK form = new FrmRDXYK();
            form.ShowDialog();
        }
    }
}
