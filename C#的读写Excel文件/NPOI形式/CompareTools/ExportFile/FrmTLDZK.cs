using CompareTools;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
using CompareTools.Common;
using CompareTools.Common.Enumeration;
using CompareTools.Business;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmDuiZhangDan : Form
    {
        public FrmDuiZhangDan()
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
            FolderHelper.GetFolderPath("请选择文件所在的目录路径", this.txtFolderPath);
        }

        /// <summary>
        /// 读取目录下的文件取出数据 & 创建新的文件 & 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            //得到选择的商户
            List<string> selectTraderList = DataGet.GetSelectedTrader(this.clbTraderList);
            if (selectTraderList.Count == 0)
            {
                MessageBox.Show("请选择要导出的商户！");
                return;
            }

            string sheetName = string.Empty;
            string currentDate = string.Empty;//创建时间
            string totalTransMoney = string.Empty;//交易金额
            string totalTransFree = string.Empty;//手续费
            string totalCount = string.Empty;//笔数
            Dictionary<string, bool> createResult = new Dictionary<string, bool>();//创建结果
            StringBuilder resultMessage = new StringBuilder();//结果消息

            //源目录和输出目录都不能为空
            if (!string.IsNullOrEmpty(this.txtFolderPath.Text) && !string.IsNullOrEmpty(this.txtOutput.Text))
            {
                //导出的数据
                Dictionary<string, List<string>> outputData = new Dictionary<string, List<string>>();
                DirectoryInfo sourceFolder = new DirectoryInfo(this.txtFolderPath.Text);
                //输出文件路径
                string outputFilePath = FileHelper.GetCurentDayFilePath(this.txtOutput.Text, "通联对账单");
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();
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
                    
                    //得到数据
                    DataGet.GetFileDataByDuiZhang(sourceFilePath,
                                                    out sheetName,
                                                    out currentDate,
                                                    out totalTransMoney,
                                                    out totalTransFree);
                    //没有选择全部商户 & 选择的商户和商户号不匹配不导出
                    if (!selectTraderList.Contains("TraderSelect") && !selectTraderList.Contains(sheetName))
                    {
                        continue;
                    }
                    //如果包含这个商户号,就在这个key基础上增加数据
                    if (outputData.Keys.Contains(sheetName))
                    {
                        outputData[sheetName].Add(currentDate + "|" + totalTransMoney + "|" + totalTransFree);
                    }
                    else//创建新的key和其值
                    {
                        List<string> value = new List<string>();
                        value.Add(currentDate + "|" + totalTransMoney + "|" + totalTransFree);
                        outputData.Add(sheetName, value);
                    }
                }

                //根据商户号创建.xls的sheet
                foreach (KeyValuePair<string, List<string>> data in outputData)
                {
                    bool isSuccessed = Utility.CreateExcel(ExcelFileType.通联对账单,outputFilePath, wk, data.Key, data);
                    createResult.Add(data.Key, isSuccessed);
                }

                //弹出提示
                if (createResult.All(r => r.Value == true))
                {
                    foreach (KeyValuePair<string, bool> result in createResult.Where(r => r.Value == true))
                    {
                        resultMessage.Append(result.Key + "\r\n");
                    }
                    MessageBox.Show("导出全部成功！商户号如下" + "\r\n" + resultMessage.ToString());
                }
                else
                {
                    foreach (KeyValuePair<string,bool> result in createResult.Where(r=>r.Value==false))
                    {
                        resultMessage.Append(result.Key + "\r\n");
                    }
                    MessageBox.Show("导出失败的商户号Sheet有" + "\r\n" + resultMessage.ToString());
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
            FolderHelper.GetFolderPath("请选择输出的目录路径", this.txtOutput);
        }

        private void FrmDuiZhangDan_Load(object sender, EventArgs e)
        {
            //绑定商户
            DataGet.BindTraderList("Trader3.xml", this.clbTraderList);
        }
    }
}
