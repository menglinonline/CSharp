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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerchantsinfoBankCardTrans
{
    public partial class FrmRDXYK : Form
    {
        public FrmRDXYK()
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
            Dictionary<string, bool> createResult = new Dictionary<string, bool>();//创建结果
            string showResult = string.Empty;
            StringBuilder sb = new StringBuilder();

            bool isFinded = false;//是否找到'终端编号'
            bool isSelectedTrader = false;//是否是用户选择的商户
            Regex regStart = new Regex(@"==============================");
            Regex regTrader = new Regex(@"商户编号：\s*(\S{1,})");
            Regex regDate = new Regex(@"清算日期：\s*(\S{1,})");
            string traderNo = "";//商户号
            string settleDate = "";//结算日期
           
            //源目录和输出目录都不能为空
            if (!string.IsNullOrEmpty(this.txtOutput.Text) && !string.IsNullOrEmpty(this.txtFolderPath.Text))
            {
                //商户账号集合
                Dictionary<string, List<string>> listTraderCardAccount = new Dictionary<string, List<string>>();
                DirectoryInfo sourceFolder = new DirectoryInfo(this.txtFolderPath.Text);
                //输出文件路径
                string outputFilePath = FileHelper.GetCurentDayFilePath(this.txtOutput.Text, "银联RD信用卡");
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                }
               
                //遍历源目录下的所有.rd文件,找到用户选择的商户的所有'主账号'列
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
                            isSelectedTrader = false;//到头部表示另起一个片段,所以false
                        }
                        //商户号
                        Match regTenantMatch = regTrader.Match(strLine);
                        if (regTenantMatch.Success)
                        {
                            traderNo = regTenantMatch.Groups[1].Value;
                            //用户选择的商户包含读取到的商户，就是要获取的部分
                            if (selectTraderList.Contains(traderNo))
                            {
                                isSelectedTrader = true;
                            }
                        }
                        //日期
                        Match regDateMatch = regDate.Match(strLine);
                        if (regDateMatch.Success)
                        {
                            settleDate = regDateMatch.Groups[1].Value;
                        }
                        /* 因为片段是以下格式
                         * 终端编号 主账号
                         * 
                         * _
                         * 
                         * 所需数据
                         * 终端编号
                         */
                        //isFinded为true,表示之前已找到'终端编号',本次找到'终端编号',则表示是结尾的'终端编号'则退出,进入下一个循环
                        if (strLine.StartsWith(@"终端编号") && isFinded)
                        {
                            isFinded = false;
                            continue;
                        }

                        //找不到"终端编号"则退出,进入下一循环
                        if (!strLine.StartsWith(@"终端编号") && !isFinded)
                        { 
                            continue;
                        }
                        else //isFinded为false,表示之前没有找到'终端编号',此次找到'终端编号',则表示是第一次找到
                        {
                            isFinded = true;
                        }

                        //只记录用户选择商户的信息'主账号'列
                        //不需要''行,不需要'_'行,'终端编号'行
                        if (!strLine.StartsWith(@"终端编号") && !string.IsNullOrWhiteSpace(strLine)
                            && !strLine.StartsWith(@"_") && isSelectedTrader)
                        {
                            string cardAccount = strLine.Substring(28, 6);//支付卡号
                            string transMoney = strLine.Substring(78, 15).Trim();//交易金额
                            string traderFree = string.Empty;
                            string settleMoney = string.Empty;
                            if (transMoney.Length >= 9)
                            {
                                traderFree = strLine.Substring(94, 13).Trim();//商户费用
                            }
                            else
                            {
                                traderFree = strLine.Substring(90, 13).Trim();//商户费用
                            }
                            if (traderFree.Length >= 9)
                            {
                                settleMoney = strLine.Substring(110, 13).Trim();//结算金额
                            }
                            else if (traderFree.Length >= 6 && traderFree.Length < 9)
                            {
                                settleMoney = strLine.Substring(106, 13).Trim();//结算金额
                            }
                            else
                            {
                                settleMoney = strLine.Substring(112, 10).Trim();//结算金额
                            }
                           
                            //如果包含这个商户号,就在这个key基础上增加数据
                            if (listTraderCardAccount.Keys.Contains(traderNo))
                            {
                                listTraderCardAccount[traderNo].Add(cardAccount + "|" + settleDate + "|" + transMoney + "|" + traderFree + "|" + settleMoney);
                            }
                            else//创建新的key和其值
                            {
                                List<string> cardAccountList = new List<string>();
                                cardAccountList.Add(cardAccount + "|" + settleDate + "|" + transMoney + "|" + traderFree + "|" + settleMoney);
                                listTraderCardAccount.Add(traderNo, cardAccountList);
                            }
                        }
                    }
                }
                //遍历所以*.xls文件,用listAccount去查找匹配的行
                foreach (FileInfo file in sourceFolder.GetFiles("*.xls"))
                {
                    //源文件路径
                    string sourceFilePath = sourceFolder + "\\" + file.Name;
                    //遍历商户号
                    foreach (KeyValuePair<string, List<string>> cardAccount in listTraderCardAccount)
                    {
                        Dictionary<string, List<string>> outputData = new Dictionary<string, List<string>>();
                        //遍历商户号下的主账号
                        foreach (string cardNo in cardAccount.Value)
                        {
                            /* cardNo格式
                             * 付款卡号|结算日期|交易金额|商户费用|结算金额
                             * */
                            string[] cardInfoArr = cardNo.Split('|');
                            if (cardInfoArr.Length == 5)
                            {
                                string curCardNo = cardInfoArr[0];
                                string curSetDate = cardInfoArr[1];
                                decimal curTransMoney = Convert.ToDecimal(cardInfoArr[2]);
                                decimal curTransFree = Convert.ToDecimal(cardInfoArr[3]);
                                decimal curSettleMoney = Convert.ToDecimal(cardInfoArr[4]);
                                //判断付款卡号是不是信用卡
                                bool isXYCard = DataGet.IsXYCard(sourceFilePath, curCardNo);
                                if (isXYCard)
                                {
                                    /* outputData value格式
                                    * 交易金额|商户费用|结算金额|笔数
                                    * */
                                    //如果已存在某个时间
                                    if (outputData.Keys.Contains(curSetDate))
                                    {
                                        //取出原来的值
                                        List<string> orgValue = outputData[curSetDate];
                                        foreach (string org in orgValue)
                                        {
                                            string[] orgValueArr = org.Split('|');
                                            if (orgValueArr.Length == 4)
                                            {
                                                Decimal sumTransMoney = Convert.ToDecimal(orgValueArr[0]) + curTransMoney;
                                                Decimal sumTransFree = Convert.ToDecimal(orgValueArr[1]) + curTransFree;
                                                Decimal sumSettleMoney = Convert.ToDecimal(orgValueArr[2]) + curSettleMoney;
                                                //表示是一天需要合计
                                                List<string> list = new List<string>();
                                                list.Add(sumTransMoney.ToString()  + "|" + sumTransFree.ToString() + "|" + sumSettleMoney + "|" + (Convert.ToInt16(orgValueArr[3]) + 1));
                                                outputData[curSetDate] = list;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        List<string> list = new List<string>();
                                        list.Add(curTransMoney + "|" + curTransFree + "|" + curSettleMoney + "|" + 1);
                                        outputData.Add(curSetDate, list);
                                    }
                                }
                            }
                        }

                        //合计行
                        decimal totalTransMoney = 0M;//交易金额合计
                        decimal totalTransFree = 0M;//手续费合计
                        decimal totalSettleMoney = 0M;//结算金额合计
                        int totalCount = 0;//笔数合计
                       
                        if (outputData.Keys.Count > 0)
                        {
                            foreach (KeyValuePair<string, List<string>> data in outputData)
                            {
                                string[] moneyArr = data.Value[0].Split('|');
                                totalTransMoney += Convert.ToDecimal(moneyArr[0]);
                                totalTransFree += Convert.ToDecimal(moneyArr[1]);
                                totalSettleMoney += Convert.ToDecimal(moneyArr[2]);
                                totalCount += Convert.ToInt16(moneyArr[3]);
                            }
                        }
                        List<string> totalList = new List<string>();
                        totalList.Add(totalTransMoney + "|" + totalTransFree + "|" + totalSettleMoney + "|" + totalCount);
                        outputData.Add("合计", totalList);

                        createResult = Utility.CreateExcel(ExcelFileType.银联RD信用卡, outputFilePath, wk, cardAccount.Key, outputData);
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
            DataGet.BindTraderList("Trader7.xml", this.clbTraderList);
        }
    }
}
