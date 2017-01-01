using CompareTools.Business;
using CompareTools.Common;
using CompareTools.Common.Enumeration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmAll : Form
    {
        public FrmAll()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }



     


       


        /// <summary>
        /// 选择输出目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择输出的目录路径", this.txtOutput);
        }

        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string sheetName = string.Empty;
            Dictionary<string, bool> createResult = new Dictionary<string, bool>();
            string showResult = string.Empty;

            //源目录和输出目录都不能为空
            if (!string.IsNullOrEmpty(this.txtFolderPath.Text)
                || !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();
                DirectoryInfo sourceFolder = new DirectoryInfo(this.txtFolderPath.Text);
                //输出文件路径
                string outputFilePath = FileHelper.GetCurentDayFilePath(this.txtOutput.Text, "日日结&随心付");
                //输出文件路径
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                }
                //遍历源目录下的所有.xls文件
                foreach (FileInfo file in sourceFolder.GetFiles("*.xls"))
                {
                    //源文件路径
                    string sourceFilePath = sourceFolder + "\\" + file.Name;
                    string[] arrFileName = file.Name.Split('-');
                    //得到日日结&随心付数据
                    Dictionary<string, List<string>> outputData = DataGet.GetFileDataByRRJBySXF(sourceFilePath, out sheetName);
                    if (arrFileName.Length == 2 && sheetName.Contains("随心付"))
                    {
                        sheetName = "随心付_" + arrFileName[1];
                    }
                    createResult = Utility.CreateExcel(ExcelFileType.日日结随心付, outputFilePath, wk, sheetName, outputData);
                }
                //弹出提示
                if (createResult.All(r => r.Value == true))
                {
                    showResult = "导出行全部成功！" + "\r\n";
                }
                else
                {
                    foreach (KeyValuePair<string, bool> result in createResult.Where(r => r.Value == false))
                    {
                        sb.Append(result.Key + "\r\n");
                    }
                    showResult = "导出行失败的日期有" + "\r\n" + sb.ToString() + "\r\n";
                }
                MessageBox.Show(showResult);
            }
            else
            {
                MessageBox.Show("请选择导入目录和输出目录！");
            }
        }

        private void btnSelectFolderName_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择文件所在的目录路径", this.txtFolderPath);
        }

    }


}
