using CompareTools.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmFile : Form
    {
        private long count = 0;
        private bool hasCount = false;
        public FrmFile()
        {
            InitializeComponent();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            LoadFile(sender);
        }

        private void LoadFile(object sender)
        { 
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\Patch";
            openFileDialog1.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    string name = ((Button)sender).Name;
                    string currentIndex = name.Replace("btnLoadFile", "");
                    (this.Controls.Find("tbFileName" + currentIndex, true)[0] as TextBox).Text = openFileDialog1.FileName + "|" + openFileDialog1.SafeFileName;
                }
                else
                {
                    MessageBox.Show("请选择要加载的文件！");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
       

        /// <summary>
        /// 确定次数 & 创建button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            hasCount = Int64.TryParse(this.tbFileCount.Text, out count);
            if (count > 0 && hasCount && !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                for (int i = 1; i <= count; i++)
                {
                    TextBox tb = new TextBox();
                    tb.Location = new System.Drawing.Point(280, 25 + i * 30);
                    tb.Name = "tbFileName" + i;
                    tb.Text = "请选择要读取的文件";
                    tb.Size = new System.Drawing.Size(250, 21);
                    this.Controls.Add(tb);

                    Button button = new Button();
                    button.Location = new System.Drawing.Point(543, 25 + i * 30);
                    button.Name = "btnLoadFile" + i;
                    button.Size = new System.Drawing.Size(75, 23);
                    button.Text = ConfigurationManager.AppSettings["btnLoadFileName"] + i;
                    button.Click += new System.EventHandler(this.btnUploadFile_Click);
                    this.Controls.Add(button);
                }
            }
            else
            {
                hasCount = false;
                MessageBox.Show("请填写正确数量且选择输出目录！");
            }
        }

        /// <summary>
        /// 获取所有的加载文件的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGatData_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            if (count > 0 && hasCount && !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                for (int i = 1; i <= count; i++)
                {
                    string filePath = (this.Controls.Find("tbFileName" + i, true)[0] as TextBox).Text;
                    if (!string.IsNullOrEmpty(filePath) && filePath != "请选择要读取的文件")
                    {
                        string[] fileName = filePath.Split('|');
                        if (fileName.Length == 2)
                        {
                            string[] sheetName = fileName[1].Split('_');
                            if (sheetName.Length == 4)
                            {
                                //读取Excel文件填充到DataSet
                                DataSet ds = Utility.ExcelToDS(fileName[0], sheetName[2]);
                                if (ds != null
                                    && ds.Tables.Count > 0
                                    && ds.Tables[0].Rows.Count > 0)
                                {
                                    //创建Excel文件
                                    //bool isSuccessed =Utility.CreateExcelFile(this.txtOutput.Text, fileName[1],
                                                                           // ds.Tables[0].Rows[1][10].ToString(),
                                                                           //  ds.Tables[0].Rows[6][4].ToString(),
                                    bool isSuccessed = false;               // ds.Tables[0].Rows[6][7].ToString());
                                    if (isSuccessed)
                                    {
                                        this.richTextBox1.Text += "文件名：";
                                        this.richTextBox1.Text += fileName[1] + " ";
                                        this.richTextBox1.Text += ds.Tables[0].Rows[1][7].ToString() + ":";//结算日期
                                        this.richTextBox1.Text += ds.Tables[0].Rows[1][10].ToString() + " ";//日期
                                        this.richTextBox1.Text += ds.Tables[0].Rows[6][0].ToString();//汇总
                                        this.richTextBox1.Text += ds.Tables[0].Rows[4][4].ToString() + ":";//交易本金
                                        this.richTextBox1.Text += ds.Tables[0].Rows[6][4].ToString() + " ";//交易本金值
                                        this.richTextBox1.Text += ds.Tables[0].Rows[4][7].ToString() + ":";//手续费
                                        this.richTextBox1.Text += ds.Tables[0].Rows[6][7].ToString();//手续费值
                                        this.richTextBox1.Text += "\r\n";
                                    }
                                    else
                                    {
                                        MessageBox.Show("数据格式不对！");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请填写正确数量且选择输出目录并单击指定文件数量按钮！");
            }
        }

        /// <summary>
        /// 选择输出目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择生成的文件的输出目录路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOutput.Text = dialog.SelectedPath;
            }
        }

    }
}
