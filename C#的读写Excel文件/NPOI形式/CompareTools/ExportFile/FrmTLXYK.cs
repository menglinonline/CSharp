using CompareTools.Business;
using CompareTools.Common;
using MerchantsinfoBankCardTrans;
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
using System.Windows.Forms;

namespace CompareTools
{
    public partial class FrmTLXYK : Form
    {
        public FrmTLXYK()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            //得到选择的商户
            List<string> selectTraderList = DataGet.GetSelectedTrader(this.clbTraderList);
            if(selectTraderList.Count == 0)
            {
                MessageBox.Show("请选择要导出的商户！");
                return;
            }
            decimal sumTransMoney = 0M;
            decimal sumTransFree = 0M;
            int sumCount = 0;
            decimal totalTransMoney = 0M;
            decimal totalTransFree = 0M;
            int totalCount = 0;
            Dictionary<string, bool> listMerSuccessed = new Dictionary<string, bool>();
            StringBuilder sb = new StringBuilder();
            bool isTotalSuccessed = false;

            //源目录和输出目录都不能为空
            if (!string.IsNullOrEmpty(this.txtFolderPath.Text) && !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                DirectoryInfo sourceFolder = new DirectoryInfo(this.txtFolderPath.Text);
                //输出文件路径
                string outputFilePath = FileHelper.GetCurentDayFilePath(this.txtOutput.Text, "通联信用卡");
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();
                //输出文件路径
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                }
              
                //遍历源目录下的所有.xls文件
                foreach (FileInfo file in sourceFolder.GetFiles("*.xls"))
                {
                    if (!file.Name.Contains("xls"))
                    {
                        continue;
                    }
                    //源文件路径
                    string sourceFilePath = sourceFolder + "\\" + file.Name;
                    string[] arrFileName = file.Name.Split('_');
                    if (arrFileName.Length == 4)
                    {
                        string sheetName = arrFileName[2];
                        //没有选择全部商户 & 选择的商户和商户号不匹配不导出
                        if(!selectTraderList.Contains("TraderSelect") && !selectTraderList.Contains(sheetName))
                        {
                            continue;
                        }
                        //得到通联信用卡数据
                        DataTable dt = DataGet.GetFileDataByTLXYK(sourceFilePath, out sumTransMoney, out sumTransFree, out sumCount);
                        totalTransMoney += sumTransMoney;//总计的交易额
                        totalTransFree += sumTransFree;//总计的手续费
                        totalCount += sumCount;//总计的笔数
                        if (dt.Rows.Count > 0)
                        {
                            //有相同的商户号只有一个Sheet追加行
                            ISheet sheet = wk.GetSheet(sheetName);
                            if (sheet != null)
                            {
                                if (sheetName == sheet.SheetName)
                                {
                                    /*写行
                                    在原有Sheet上追加行*/
                                    Utility.CreateSheetRow(outputFilePath, wk, sheetName, dt);
                                }
                            }
                            else//创建新的工作表
                            {
                                /*写new sheet
                                创建Excel文件 & 在工作薄上创建多个Sheet 多个商户号则多个Sheet*/
                                bool isSuccessed = Utility.CreateExcelSheet(outputFilePath, wk, sheetName, dt);
                                listMerSuccessed.Add(sheetName, isSuccessed);
                            }
                        }
                    }                    
                }
                //写统计 sheet
                int count = sourceFolder.GetFiles("*.xls").Count();
                isTotalSuccessed = Utility.CreateExcelSheet(outputFilePath, wk, count + "个文件的总计", totalTransMoney, totalTransFree, totalCount);

                //弹出提示
                if (listMerSuccessed.All(r => r.Value == true))
                {
                    foreach (KeyValuePair<string, bool> result in listMerSuccessed.Where(r => r.Value == true))
                    {
                        sb.Append(result.Key + "\r\n");
                    }
                    MessageBox.Show("导出全部成功！商户号如下" + "\r\n" + sb.ToString());
                }
                else
                {
                    foreach (KeyValuePair<string, bool> result in listMerSuccessed.Where(r => r.Value == false))
                    {
                        sb.Append(result.Key + "\r\n");
                    }
                    MessageBox.Show("导出失败的商户号Sheet有" + "\r\n" + sb.ToString());
                }
            }
            else
            {
                MessageBox.Show("请选择导入目录和输出目录！");
                return;
            }
        }

        private void btnSelectFolderName_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择文件所在的目录路径", this.txtFolderPath);
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择输出的目录路径", this.txtOutput);
        }

        private void FrmTLXYK_Load(object sender, EventArgs e)
        {
            //绑定商户
            DataGet.BindTraderList("Trader5.xml", this.clbTraderList);
        }
    }
}
