using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Xml;

namespace AccountChecking
{
    public partial class FrmSingleCheck : Form
    {
        private const string CONST_XPath = "trader";
        private string xmlFileName = System.Environment.CurrentDirectory + "/Trader/Trader1.xml";
        public FrmSingleCheck()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (lstFileYH.Items.Count > 0 && lstFileST.Items.Count > 0)
            {
                if (savedlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        decimal merRate = 0m;
                        if (String.IsNullOrWhiteSpace(txtRate.Text))
                            MessageBox.Show("请输入手续费率！");
                        else if (!Decimal.TryParse(txtRate.Text, out merRate))
                            MessageBox.Show("请输入正确的手续费率！");
                        else
                        {
                            decimal merCent = 0m;
                            if (String.IsNullOrWhiteSpace(txtCent.Text))
                                MessageBox.Show("请输入手续费率微调比率！");
                            else if (!Decimal.TryParse(txtCent.Text, out merCent))
                                MessageBox.Show("请输入正确的手续费率微调比率！");
                            merRate = merRate + merCent;
                            string traderCode = cbxTrader.SelectedValue.ToString();
                            if (radTenant.Checked)
                            {
                                string encodeType = radGB2312.Checked ? "GB2312" : "UTF8";
                                SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(),
                                                                  lstFileST.Items.Cast<string>(),
                                                                  savedlg.FileName, traderCode, 1, merRate, encodeType);
                                compare.Compare();
                                MessageBox.Show("结束");
                            }
                            else if (radUnoin.Checked)
                            {
                                SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(),
                                                                  lstFileST.Items.Cast<string>(),
                                                                  savedlg.FileName, traderCode, 2, merRate);
                                compare.Compare();
                                MessageBox.Show("结束");

                            }
                            else if (radSpecial.Checked)
                            {
                                SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(),
                                                                  lstFileST.Items.Cast<string>(),
                                                                  savedlg.FileName, traderCode, 3, merRate);
                                compare.Compare();
                                MessageBox.Show("结束");
                            }
                            else if (radQtopay.Checked)
                            {
                                SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(),
                                                                  lstFileST.Items.Cast<string>(),
                                                                  savedlg.FileName, traderCode, 4, merRate);
                                compare.Compare();
                                MessageBox.Show("结束");
                            }
                            else if (radZSF.Checked)
                            {
                                SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(),
                                                                  lstFileST.Items.Cast<string>(),
                                                                  savedlg.FileName, traderCode, 5, merRate);
                                compare.Compare();
                                MessageBox.Show("结束");
                            }
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("保存文件正在使用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
            }
        }

        private void btnSelectFileYH_Click(object sender, EventArgs e)
        {
            DialogResult result = opendlgYH.ShowDialog();
            if (result == DialogResult.OK)
            {
                //lstFileYH.Items.Clear();
                //lstFileYH.Items.AddRange(opendlgYH.FileNames);
                AddYHFiles(opendlgYH.FileNames);
            }
        }
        private void AddYHFiles(string[] files)
        {
            foreach (var f in files)
            {
                if (!lstFileYH.Items.Contains(f))
                {
                    lstFileYH.Items.Add(f);
                }
            }
        }
        private void btnClearFileYH_Click(object sender, EventArgs e)
        {
            lstFileYH.Items.Clear();
        }

        private void btnSelectFileST_Click(object sender, EventArgs e)
        {
            DialogResult result = opendlgST.ShowDialog();
            if (result == DialogResult.OK)
            {
                AddSTFiles(opendlgST.FileNames);
            }
        }

        private void AddSTFiles(string[] files)
        {
            foreach (var f in files)
            {
                if (!lstFileST.Items.Contains(f))
                    lstFileST.Items.Add(f);
            }
        }

        private void btnClearFileST_Click(object sender, EventArgs e)
        {
            lstFileST.Items.Clear();
        }

        private void lstFileYH_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files != null && files.Length > 0)
            {
                //lstFileYH.Items.Clear();
                //lstFileYH.Items.Add(files[0]);
                AddYHFiles(files);
            }
        }

        private void lstFileST_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files != null && files.Length > 0)
            {
                AddSTFiles(files);
            }
        }

        private void lstFileYH_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                lstFileYH.Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lstFileST_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                lstFileST.Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                BindTrader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序发生错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 绑定商户
        /// </summary>
        private void BindTrader()
        {
            DataTable bind = ReadXmlNode(xmlFileName, CONST_XPath);
            this.cbxTrader.DataSource = bind;
            this.cbxTrader.DisplayMember = "Text";
            this.cbxTrader.ValueMember = "Value";
            this.cbxTrader.SelectedIndex = 0;
        }

        ///<summary>
        /// 选择匹配XPath表达式的所有子节点XmlNode.
        ///</summary>
        ///<param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        ///<param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        ///<returns>返回XmlNode</returns>
        private DataTable ReadXmlNode(string xmlFileName, string xpath)
        {
            DataSet d = new DataSet(); //创建DataSet
            d.Tables.Add("trader");
            d.Tables["trader"].Columns.Add("Text");
            d.Tables["trader"].Columns.Add("Value");

            DataRow all = d.Tables["trader"].NewRow();
            all["Text"] = "所有商户";
            all["Value"] = "TraderSelect";
            d.Tables["trader"].Rows.Add(all);

            try
            {
                XmlDocument doc = new XmlDocument();

                // 获得配置文件的全路径　　
                doc.Load(xmlFileName);
                XmlNode node = doc.SelectSingleNode(xpath);
                XmlNodeList nodes = node.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].NodeType == XmlNodeType.Element)
                    {
                        DataRow row = d.Tables["trader"].NewRow();
                        row["Text"] = nodes[i].Attributes["name"].Value;
                        row["Value"] = nodes[i].InnerText;
                        d.Tables["trader"].Rows.Add(row);
                    }
                }
                return d.Tables["trader"];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void radTenant_CheckedChanged(object sender, EventArgs e)
        {
            radioBtnLanguageShow(true);
        }

        private void radUnoin_CheckedChanged(object sender, EventArgs e)
        {
            radioBtnLanguageShow(false);
        }

        private void radSpecial_CheckedChanged(object sender, EventArgs e)
        {
            radioBtnLanguageShow(false);
        }

        private void radQtopay_CheckedChanged(object sender, EventArgs e)
        {
            radioBtnLanguageShow(false);
        }
        private void radZSF_CheckedChanged(object sender, EventArgs e)
        {
            radioBtnLanguageShow(false);
        }

        private void radioBtnLanguageShow(bool isShow)
        {
            radUTF8.Visible = isShow;
            radGB2312.Visible = isShow;
            //默认系统语言设置GB2312
            radGB2312.Checked = true;
        }

        private void cbxTrader_SelectedIndexChanged(object sender, EventArgs e)
        {
            string traderCode = cbxTrader.SelectedValue.ToString();
            if ("TraderSelect".Equals(traderCode))
                ctrPanel.Visible = false;
            else
                ctrPanel.Visible = true;
        }

       
              
    }
}
