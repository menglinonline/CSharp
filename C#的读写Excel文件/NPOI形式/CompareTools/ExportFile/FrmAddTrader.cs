using CompareTools.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CompareTools
{
    public partial class FrmAddTrader : Form
    {
        public FrmAddTrader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTrader_Click(object sender, EventArgs e)
        {
            List<string> fileNameList = GetXmlNameList();
            if (!string.IsNullOrWhiteSpace(this.txtTraderName.Text) && !string.IsNullOrWhiteSpace(this.txtTraderNo.Text)
                && fileNameList.Count > 0)
            {
                //检查是否存在相同的商户
                if (CheckExist(this.txtTraderName.Text.Trim(), this.txtTraderNo.Text.Trim()))
                {
                    MessageBox.Show("该商户已存在！");
                    return;
                }
                try
                {
                    foreach (string fileName in fileNameList)
                    {
                        //添加商户
                        XmlHelper.AddTrader(fileName, Const.CONST_XPath, this.txtTraderName.Text.Trim(), this.txtTraderNo.Text.Trim());
                    }
                    //读取商户列表
                    BindTrader(fileNameList);
                    MessageBox.Show("填加成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("填加异常！");
                }
            }
            else
            {
                MessageBox.Show("请填写商户名和商户号，以及选择菜单！");
            }
        }

        /// <summary>
        /// 检查是否存在相同的商户
        /// </summary>
        /// <param name="traderName">商户名</param>
        /// <param name="traderNo">商户号</param>
        /// <returns></returns>
        private bool CheckExist(string traderName, string traderNo)
        {
            if (this.lbTraderList.Items.Count > 0)
            {
                foreach (DataRowView row in this.lbTraderList.Items)
                {
                    if (row.Row[this.lbTraderList.DisplayMember].ToString() == traderName || row.Row[this.lbTraderList.ValueMember].ToString() == traderNo)
                    {
                        return true;
                    }
                }
            }
           
            return false;
        }

        /// <summary>
        /// 绑定商户列表
        /// </summary>
        /// <param name="fileNameList">xml文件名称列表</param>
        private void BindTrader(List<string> fileNameList)
        {
            DataTable dt = new DataTable();
            dt = XmlHelper.GetTraderListByXmlPath(fileNameList, Const.CONST_XPath);
            this.lbTraderList.DataSource = dt;
            this.lbTraderList.DisplayMember = "Text";
            this.lbTraderList.ValueMember = "Value";
            this.lbTraderList.SelectedIndex = 0;
        }

        /// <summary>
        /// 根据单选按钮得到*.xml文件名称列表
        /// </summary>
        /// <returns>*.xml文件名称列表</returns>
        private List<string> GetXmlNameList()
        {
            List<string> fileNameList = new List<string>();
            if (this.cbTrader1.Checked)
            {
                fileNameList.Add("Trader1.xml");
            }
            else
            {
                fileNameList.Remove("Trader1.xml");
            }
            if (this.cbTrader2.Checked)
            {
                fileNameList.Add("Trader2.xml");
            }
            else
            {
                fileNameList.Remove("Trader2.xml");
            }
            if (this.cbTrader3.Checked)
            {
                fileNameList.Add("Trader3.xml");
            }
            else
            {
                fileNameList.Remove("Trader3.xml");
            }
            if (this.cbTrader4.Checked)
            {
                fileNameList.Add("Trader4.xml");
            }
            else
            {
                fileNameList.Remove("Trader4.xml");
            }
            if (this.cbTrader5.Checked)
            {
                fileNameList.Add("Trader5.xml");
            }
            else
            {
                fileNameList.Remove("Trader5.xml");
            }
            if (this.cbTrader6.Checked)
            {
                fileNameList.Add("Trader6.xml");
            }
            else
            {
                fileNameList.Remove("Trader6.xml");
            }
            if (this.cbTrader7.Checked)
            {
                fileNameList.Add("Trader7.xml");
            }
            else
            {
                fileNameList.Remove("Trader7.xml");
            }

            return fileNameList;
        }

        /// <summary>
        /// 删除商户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTrader_Click(object sender, EventArgs e)
        {
            if (this.lbTraderList.Items.Count > 0)
            {
                DataRowView drView = this.lbTraderList.SelectedItem as DataRowView;
                string traderName = drView[this.lbTraderList.DisplayMember].ToString();
                string traderNo = drView[this.lbTraderList.ValueMember].ToString();
                if (traderName == "所有商户")
                {
                    MessageBox.Show("系统商户不能删除！");
                    return;
                }
                List<string> fileNameList = GetXmlNameList();
                try
                {
                    foreach (string fileName in fileNameList)
                    {
                        //删除商户
                        XmlHelper.DeleteXmlNodeByTrader(fileName, Const.CONST_XPath, traderName, traderNo);
                    }
                    //读取商户列表
                    BindTrader(fileNameList);
                    MessageBox.Show("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败！");
                }
            }
            else
            {
                MessageBox.Show("没有商户删除！");
            }
        }

        private void cbTrader_Click(object sender, EventArgs e)
        {
            List<string> fileNameList = GetXmlNameList();
            BindTrader(fileNameList);
        }

        private void FrmAddTrader_Load(object sender, EventArgs e)
        {

        }
    }
}
