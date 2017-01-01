using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmFolder : Form
    {
        public FrmFolder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFolderName_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件的目录路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFolderPath.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 读取目录下的文件取出数据 & 创建新的文件 & 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtFolderPath.Text) && !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                this.richTextBox1.Text = "";
                DirectoryInfo folder = new DirectoryInfo(this.txtFolderPath.Text);
                //获取选择目录下的所以xls文件
                foreach (FileInfo file in folder.GetFiles("*.xls"))
                {
                    string filePath = this.txtFolderPath.Text + "\\" + file.Name;
                    string[] sheetName = file.Name.Split('_');
                    if (sheetName.Length == 4)
                    {
                        //读取Excel文件填充到DataSet
                        DataSet ds = Utility.ExcelToDS(filePath, sheetName[2]);
                        if (ds != null
                            && ds.Tables.Count > 0
                            && ds.Tables[0].Rows.Count > 0)
                        {
                            //创建Excel文件
                            bool isSuccessed = Utility.CreateExcelFile(this.txtOutput.Text,file.Name, 
                                                                        ds.Tables[0].Rows[1][10].ToString(),
                                                                        ds.Tables[0].Rows[6][4].ToString(),
                                                                        ds.Tables[0].Rows[6][7].ToString());
                            if (isSuccessed)
                            {
                                this.richTextBox1.Text += "文件名：";
                                this.richTextBox1.Text += file.Name + " ";
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
                                MessageBox.Show("创建文件失败！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("数据格式不对！");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择导入目录和输出目录！");
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
