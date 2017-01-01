using CompareTools.Business;
using CompareTools.Common;
using CompareTools.Common.Enumeration;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmRD : Form
    {
        public FrmRD()
        {
            InitializeComponent();
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择文件所在的目录路径", this.txtOutput);
        }

        private void btnOuoutFile_Click(object sender, EventArgs e)
        {
            //得到选择的商户
            List<string> selectTraderList = DataGet.GetSelectedTrader(this.clbTraderList);
            if(selectTraderList.Count == 0)
            {
                MessageBox.Show("请选择要导出的商户！");
                return;
            }

            Regex regStart = new Regex(@"==============================");
            Regex regTrader = new Regex(@"商户编号：\s*(\S{1,})");
            Regex regDate = new Regex(@"清算日期：\s*(\S{1,})");
            Regex regSubtotal = new Regex(@"小计\s*(\d{1,})\s*(\S{1,})\s*(\S{1,})\s*(\S{1,})");
            string traderNo = "";//商户号
            string settleDate = "";//结算日期
            string tranMoney = "";//交易金额
            string tranFree = "";//手续费
            string tranCount = "";//笔数
            Dictionary<string, bool> createResult = new Dictionary<string, bool>();//创建结果
            StringBuilder resultMessage = new StringBuilder();//结果消息

            //源目录和输出目录都不能为空
            if (!string.IsNullOrEmpty(this.txtOutput.Text) && !string.IsNullOrEmpty(this.txtFolderPath.Text))
            {
                //导出的数据
                Dictionary<string, List<string>> outputData = new Dictionary<string, List<string>>();//读取文件得到所需的数据
                DirectoryInfo sourceFolder = new DirectoryInfo(this.txtFolderPath.Text);
                //输出文件路径
                string outputFilePath = FileHelper.GetCurentDayFilePath(this.txtOutput.Text, "银联RD");
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                }
                //遍历源目录下的所有.rd文件
                foreach (FileInfo file in sourceFolder.GetFiles("*"))
                {
                    if (!file.Name.Contains("RD"))
                    {
                        continue;
                    }
                    //源文件路径
                    string sourceFilePath = sourceFolder + "\\" + file.Name;

                    //读取源文件数据
                    StreamReader stmReader = DataGet.GetFileDataByRD(sourceFilePath);
                    while (!stmReader.EndOfStream)
                    {
                        string strLine = stmReader.ReadLine();
                        //商户记录开始位置
                        Match regStartMatch = regStart.Match(strLine);
                        if (regStartMatch.Success)
                        {
                            //商户编号 标题行
                            strLine = stmReader.ReadLine();
                        }
                        //商户号
                        Match regTenantMatch = regTrader.Match(strLine);
                        if (regTenantMatch.Success)
                        {
                            traderNo = regTenantMatch.Groups[1].Value;
                        }
                        //日期
                        Match regDateMatch = regDate.Match(strLine);
                        if (regDateMatch.Success)
                        {
                            settleDate = regDateMatch.Groups[1].Value;
                        }
                        //小计
                        Match regSubtotalMatch = regSubtotal.Match(strLine);
                        if (regSubtotalMatch.Success)
                        {
                            tranCount = regSubtotalMatch.Groups[1].Value;
                            tranMoney = regSubtotalMatch.Groups[2].Value;
                            tranFree = regSubtotalMatch.Groups[3].Value;
                            string vaule = settleDate + "|" + tranMoney + "|" + tranFree + "|" + tranCount;
                            //如果包含这个商户号,就在这个key基础上增加数据
                            if (outputData.Keys.Contains(traderNo))
                            {
                                //因为RD文件中小计有两处相同的值，所以此处做处理
                                if(!outputData[traderNo].Contains(vaule))
                                {
                                    outputData[traderNo].Add(vaule);
                                }
                            }
                            else//创建新的key和其值
                            {
                                List<string> valueList = new List<string>();
                                valueList.Add(vaule);
                                outputData.Add(traderNo, valueList);
                            }
                           
                        }
                    }
                }
                //所有的选择商户的数据
                Dictionary<string, List<string>> allSelectTraderData = new Dictionary<string, List<string>>();
                foreach (string trader in selectTraderList)
                {
                    //根据选择的商户筛选数据
                    Dictionary<string, List<string>> selectTraderData = outputData.Where(d => d.Key.Contains(trader)).ToDictionary(o => o.Key, p => p.Value);
                    foreach (KeyValuePair<string, List<string>> data in selectTraderData)
                    {
                        allSelectTraderData.Add(data.Key, data.Value);
                    }
                }

                //根据商户号创建.xls的sheet
                foreach (KeyValuePair<string, List<string>> data in allSelectTraderData)
                {
                    bool isSuccessed = Utility.CreateExcel(ExcelFileType.银联RD, outputFilePath, wk, data.Key, data);
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
                    foreach (KeyValuePair<string, bool> result in createResult.Where(r => r.Value == false))
                    {
                        resultMessage.Append(result.Key + "\r\n");
                    }
                    MessageBox.Show("导出失败的商户号Sheet有" + "\r\n" + resultMessage.ToString());
                }
            }
            else
            {
                MessageBox.Show("请选择导入文件和输出目录！");
            }
        }

        private void btnSelectFolderName_Click(object sender, EventArgs e)
        {
            FolderHelper.GetFolderPath("请选择文件所在的目录路径", this.txtFolderPath);
        }

        private void FrmRD_Load(object sender, EventArgs e)
        {
            //绑定商户
            DataGet.BindTraderList("Trader6.xml", this.clbTraderList);
        }
    }
}
