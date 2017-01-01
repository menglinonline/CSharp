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
    public partial class FrmSingleConvert : Form
    {
        private const string CONST_XPath = "trader";
        private string xmlFileName = System.Environment.CurrentDirectory + "/Trader/Trader2.xml";

        public FrmSingleConvert()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (lstFileYH.Items.Count > 0)
            {
                try
                {
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        string strPath = folderBrowser.SelectedPath;//文件保存路径
                        string traderCode = cbxTrader.SelectedValue.ToString();
                        Encoding encode = radGB2312.Checked ? Encoding.Default : Encoding.UTF8;
                        SHCompare compare = new SHCompare(lstFileYH.Items.Cast<string>(), traderCode, strPath, encode);
                        compare.Compare();
                        MessageBox.Show("结束");
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
                    lstFileYH.Items.Add(f);
            }
        }
        private void btnClearFileYH_Click(object sender, EventArgs e)
        {
            lstFileYH.Items.Clear();
        }

        private void lstFileYH_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files != null && files.Length > 0)
                AddYHFiles(files);
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
    }
}
