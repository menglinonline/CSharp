using CompareTools.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareTools.Business
{
    public class DataGet
    {
        #region 得到通联支付银行对账单 Excel文件的数据
        /// <summary>
        /// 得到通联支付银行对账单Excel文件的数据
        /// </summary>
        /// <param name="sourceFilePath">Excel文件源路径</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="settleDateValue">结算日期</param>
        /// <param name="totalTransMoney">汇总交易本金</param>
        /// <param name="totalTransFree">汇总手续费</param>
        public static void GetFileDataByDuiZhang(string sourceFilePath,
                                                out string sheetName,
                                                out string settleDateValue,
                                                out string totalTransMoney,
                                                out string totalTransFree)
        {
            using (FileStream fs = File.OpenRead(sourceFilePath))//打开银行对账单.xls文件
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);//把xls文件中的数据写入wk中
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据
                sheetName = sheet.SheetName;

                IRow row = sheet.GetRow(2);//读取第二行
                if (row != null)
                {
                    ICell cell = row.GetCell(10);//结算日期值
                    if (cell != null)
                    {
                        settleDateValue = cell.ToString();
                    }
                    else
                    {
                        settleDateValue = string.Empty;
                    }
                }
                else
                {
                    settleDateValue = string.Empty;
                }

                row = sheet.GetRow(7);//读取当7行数据
                if (row != null)
                {
                    ICell cell = row.GetCell(4);//交易本金
                    if (cell != null)
                    {
                        totalTransMoney = cell.ToString();
                    }
                    else
                    {
                        totalTransMoney = string.Empty;
                    }
                    cell = row.GetCell(7);//手续费
                    if (cell != null)
                    {
                        totalTransFree = cell.ToString();
                    }
                    else
                    {
                        totalTransFree = string.Empty;
                    }
                }
                else
                {
                    totalTransMoney = string.Empty;
                    totalTransFree = string.Empty;
                }
            }
        }
        #endregion

        #region 得到日日结 & 随心付 Excel文件的数据
        /// <summary>
        /// 得到日日结 & 随心付 Excel文件的数据
        /// </summary>
        /// <param name="sourceFilePath">Excel文件源路径</param>
        /// <param name="sheetName">工作表名</param>
        public static Dictionary<string, List<string>> GetFileDataByRRJBySXF(string sourceFilePath, out string sheetName)
        {
            sheetName = string.Empty;
            Dictionary<string, List<string>> outputData = new Dictionary<string, List<string>>();
            using (FileStream fs = File.OpenRead(sourceFilePath))//打开.xls文件
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);//把xls文件中的数据写入wk中
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据
                //为了得到工作表名
                IRow transtype_row = sheet.GetRow(2);//读取交易类型行数据
                IRow balanceaccount_row = sheet.GetRow(6);//读取结算账户行数据
                if (transtype_row != null && balanceaccount_row != null)
                {
                    ICell transtype_cell = transtype_row.GetCell(2);//读取当前列数据
                    ICell balanceaccount_cell = balanceaccount_row.GetCell(4);//读取当前行数据
                    if (transtype_cell != null && balanceaccount_cell != null)
                    {
                        sheetName = transtype_cell.ToString() + "_" + balanceaccount_cell.ToString();
                    }
                }
                //遍历行
                for (int j = 0; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);//读取当前行数据
                    if (row != null)
                    {
                        //遍历列
                        for (int k = 0; k <= row.LastCellNum; k++)//LastCellNum是当前行的总列数
                        {
                            ICell cell = row.GetCell(k);//读取当前列数据
                            if (cell != null)
                            {
                                DateTime dd;
                                //如果列能转化为日 && 金额列也能认为能转化为日期类型(所以排除第5和第6列)
                                bool isConverted = DateTime.TryParse(cell.ToString(), out dd);
                                if (isConverted && k != 5 && k != 6)
                                {
                                    //如果已存在某个时间
                                    if (outputData.Keys.Contains(dd.ToShortDateString()))
                                    {
                                        List<string> curValue = outputData[dd.ToShortDateString()];
                                        /* 2015-04-17 300|500|1 出账金额|服务费|笔数
                                           2015-04-17 200|100|1
                                         * 处理为
                                         * 2015-04-17 500|600|1
                                         * */
                                        foreach (string str in curValue)
                                        {
                                            string[] valueArr = str.Split('|');
                                            if (valueArr.Length == 3)
                                            {
                                                Decimal sumTransMoney = Convert.ToDecimal(valueArr[0]) + Convert.ToDecimal(row.GetCell(5).ToString());
                                                Decimal sumTransFree = Convert.ToDecimal(valueArr[1]) + Convert.ToDecimal(row.GetCell(6).ToString());
                                                List<string> list = new List<string>();
                                                list.Add(sumTransMoney.ToString() + "|" + sumTransFree.ToString() + "|" + (Convert.ToInt16(valueArr[2]) + 1));
                                                outputData[dd.ToShortDateString()] = list;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        List<string> value = new List<string>();
                                        value.Add(row.GetCell(5).ToString() + "|" + row.GetCell(6).ToString() + "|" + 1);
                                        outputData.Add(dd.ToShortDateString(), value);
                                    }
                                }
                            }
                        }
                    }
                }
            }

           //合计行
           decimal totalTransMoney = 0M;//交易金额合计
           decimal totalTransFree = 0M;//手续费合计
           int totalCount = 0;//笔数合计
           
            if (outputData.Keys.Count > 0)
            {
                foreach (KeyValuePair<string, List<string>> data in outputData)
                {
                     string[] moneyArr = data.Value[0].Split('|');
                     totalTransMoney += Convert.ToDecimal(moneyArr[0]);
                     totalTransFree += Convert.ToDecimal(moneyArr[1]);
                     totalCount += Convert.ToInt16(moneyArr[2]);
                }
            }
            List<string> totalList = new List<string>();
            totalList.Add(totalTransMoney + "|" + totalTransFree + "|" + totalCount);
            outputData.Add("合计", totalList);

            return outputData;
        }
        #endregion

        #region 得到通联支付信用卡 Excel文件的数据
        /// <summary>
        /// 得到通联支付信用卡数据
        /// </summary>
        /// <param name="sourceFilePath">Excel文件源路径</param>
        /// <param name="sumTransMoney">输出的本金</param>
        /// <param name="sumTransFree">输出的手续费</param>
        /// <param name="sumCount">输出的笔数</param>
        /// <returns></returns>
        public static DataTable GetFileDataByTLXYK(string sourceFilePath, out decimal sumTransMoney, out decimal sumTransFree, out int sumCount)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("交易时间", typeof(string));
            dt.Columns.Add("交易类型", typeof(string));
            dt.Columns.Add("凭证号", typeof(string));
            dt.Columns.Add("卡号", typeof(string));
            dt.Columns.Add("卡类别", typeof(string));
            dt.Columns.Add("发卡行代码", typeof(string));
            dt.Columns.Add("发卡行名称", typeof(string));
            dt.Columns.Add("交易本金", typeof(string));
            dt.Columns.Add("手续费", typeof(string));
            dt.Columns.Add("分期手续费", typeof(string));
            dt.Columns.Add("原交易日期", typeof(string));
            dt.Columns.Add("流水号", typeof(string));

            using (FileStream fs = File.OpenRead(sourceFilePath))//打开通联支付信用卡.xls文件
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);//把xls文件中的数据写入wk中
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据
                string sheetName = sheet.SheetName;
                //遍历行
                for (int j = 10; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);//读取当前行数据
                    if (row != null)
                    {
                        ICell cell = row.GetCell(6);//读取判断列数据
                        if (cell != null && cell.ToString().Contains("贷记"))
                        {
                            DataRow newRow = dt.NewRow();
                            ICell cell1 = row.GetCell(1);
                            ICell cell2 = row.GetCell(2);
                            ICell cell3 = row.GetCell(3);
                            ICell cell4 = row.GetCell(5);
                            ICell cell5 = row.GetCell(6);
                            ICell cell6 = row.GetCell(8);
                            ICell cell7 = row.GetCell(9);
                            ICell cell8 = row.GetCell(11);
                            ICell cell9 = row.GetCell(12);
                            ICell cell10 = row.GetCell(13);
                            ICell cell11 = row.GetCell(14);
                            ICell cell12 = row.GetCell(15);
                            newRow["交易时间"] = cell1.ToString();
                            newRow["交易类型"] = cell2.ToString();
                            newRow["凭证号"] = cell3.ToString();
                            newRow["卡号"] = cell4.ToString();
                            newRow["卡类别"] = cell5.ToString();
                            newRow["发卡行代码"] = cell6.ToString();
                            newRow["发卡行名称"] = cell7.ToString();
                            newRow["交易本金"] = cell8.ToString();
                            newRow["手续费"] = cell9.ToString();
                            newRow["分期手续费"] = cell10.ToString();
                            newRow["原交易日期"] = cell11.ToString();
                            newRow["流水号"] = cell12.ToString();
                            dt.Rows.Add(newRow);
                        }
                    }
                }
                sumTransMoney = 0M;
                sumTransFree = 0M;
                sumCount = dt.Rows.Count;
                //合计行
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        decimal transMoney = 0M;
                        decimal transFree = 0M;
                        bool isConvertedByTranMoney = Decimal.TryParse(dt.Rows[i][7].ToString(), out transMoney);
                        if (isConvertedByTranMoney)
                        {
                            sumTransMoney += transMoney;
                        }

                        bool isConvertedByFree = Decimal.TryParse(dt.Rows[i][8].ToString(), out transFree);
                        if (isConvertedByFree)
                        {
                            sumTransFree += Math.Abs(transFree);
                        }
                    }
                    DataRow lastRow = dt.NewRow();
                    lastRow["交易时间"] = "合计";
                    lastRow["交易本金"] = sumTransMoney.ToString();
                    lastRow["手续费"] = sumTransFree.ToString();
                    lastRow["分期手续费"] = sumCount.ToString();
                    dt.Rows.Add(lastRow);
                }

                return dt;
            }
        }
        #endregion

        #region 得到银联 RD文件的数据
        public static StreamReader GetFileDataByRD(string sourceFilePath)
        {
            //读取文件流
            FileStream fsRead = File.OpenRead(sourceFilePath);
            //读成流
            StreamReader streamReader = new StreamReader(fsRead, Encoding.Default);

            return streamReader;
        }
        #endregion

        /// <summary>
        /// 判断是否是信用卡
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="listCardAccount">RD文件的主账号列表</param>
        /// <returns></returns>
        public static bool IsXYCard(string sourceFilePath, string cardNo)
        {
            bool isXYCard = false;
            using (FileStream fs = File.OpenRead(sourceFilePath))//打开RD信用卡.xls文件
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);//把xls文件中的数据写入wk中
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据
                string sheetName = sheet.SheetName;
                //遍历行
                for (int j = 0; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);//读取当前行数据
                    ICell cell5 = row.GetCell(5);//读取判断列数据
                    ICell cell3 = row.GetCell(3);//读取判断列数据
                    //是信用卡 & 发卡行代码等于支付账号
                    if (cell5 != null && cardNo == cell5.ToString()
                        && cell3 != null && cell3.ToString().Contains("贷记"))
                    {
                        isXYCard = true;
                        break;
                    }
                }
            }

            return isXYCard;
        }



        #region 得到银联 RD信用卡文件的数据
        /// <summary>
        /// 得到银联 RD信用卡文件的数据
        /// </summary>
        /// <param name="sourceFilePath">Excel文件源路径</param>
        /// <param name="listCardAccount">卡账号集合</param>
        /// <returns></returns>
        public static DataTable GetFileDataByRDXYK(string sourceFilePath,
                                                   KeyValuePair<string, List<string>> listCardAccount,
                                                   string value)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("结算日期", typeof(string));
            dt.Columns.Add("交易本金", typeof(string));
            dt.Columns.Add("手续费", typeof(string));
            dt.Columns.Add("笔数", typeof(string));
            dt.Columns.Add("发卡行", typeof(string));
            dt.Columns.Add("发卡行代码", typeof(string));
            dt.Columns.Add("卡种名称", typeof(string));
            dt.Columns.Add("银行卡类型", typeof(string));
            dt.Columns.Add("卡号长度", typeof(string));
            dt.Columns.Add("BIN号", typeof(string));

            using (FileStream fs = File.OpenRead(sourceFilePath))//打开RD信用卡.xls文件
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);//把xls文件中的数据写入wk中
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据
                string sheetName = sheet.SheetName;
                //遍历行
                for (int j = 0; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);//读取当前行数据
                    ICell cell5 = row.GetCell(5);//读取判断列数据
                    ICell cell3 = row.GetCell(3);//读取判断列数据
                    //是信用卡 & 发卡行代码属于主账号
                    if (cell5 != null && listCardAccount.Value.Contains(cell5.ToString())
                        && cell3 != null && cell3.ToString().Contains("贷记"))
                    {
                        DataRow newRow = dt.NewRow();

                        string[] arr = value.Split('|');
                        newRow["结算日期"] = arr[0];
                        newRow["交易本金"] = arr[1];
                        newRow["手续费"] = arr[2];
                        newRow["笔数"] = arr[3];

                        ICell cell0 = row.GetCell(0);
                        ICell cell1 = row.GetCell(1);
                        ICell cell2 = row.GetCell(2);
                        ICell cell4 = row.GetCell(4);

                        if (cell0 != null)
                        {
                            newRow["发卡行"] = cell0.ToString();
                        }
                        else
                        {
                            newRow["发卡行"] = "";
                        }

                        if (cell1 != null)
                        {
                            newRow["发卡行代码"] = cell1.ToString();
                        }
                        else
                        {
                            newRow["发卡行代码"] = "";
                        }

                        if (cell2 != null)
                        {
                            newRow["卡种名称"] = cell2.ToString();
                        }
                        else
                        {
                            newRow["卡种名称"] = "";
                        }

                        if (cell3 != null)
                        {
                            newRow["银行卡类型"] = cell3.ToString();
                        }
                        else
                        {
                            newRow["银行卡类型"] = "";
                        }

                        if (cell4 != null)
                        {
                            newRow["卡号长度"] = cell4.ToString();
                        }
                        else
                        {
                            newRow["卡号长度"] = "";
                        }
                        if (cell5 != null)
                        {
                            newRow["BIN号"] = cell5.ToString();
                        }
                        else
                        {
                            newRow["BIN号"] = "";
                        }

                        dt.Rows.Add(newRow);
                    }
                }
            }

            return dt;
        }
        #endregion

        #region 获取选择的商户
        /// <summary>
        /// 获取选择的商户
        /// </summary>
        /// <param name="clbTraderList">控件</param>
        /// <returns>选择的商户</returns>
        public static List<string> GetSelectedTrader(CheckedListBox clbTraderList)
        {
            List<string> selectTraderList = new List<string>();
            //如果'所有商户'选中
            if (clbTraderList.GetItemChecked(0))
            {
                for (int i = 0; i < clbTraderList.Items.Count; i++)
                {
                    string traderNo = ((DataRowView)clbTraderList.Items[i]).Row[clbTraderList.ValueMember].ToString();
                    selectTraderList.Add(traderNo);
                }
            }
            else
            {
                for (int i = 0; i < clbTraderList.CheckedItems.Count; i++)
                {
                    string traderNo = ((DataRowView)clbTraderList.CheckedItems[i]).Row[clbTraderList.ValueMember].ToString();
                    selectTraderList.Add(traderNo);
                }
            }
            selectTraderList.Remove("TraderSelect");

            return selectTraderList;
        }
        #endregion

        #region 绑定商户列表
        /// <summary>
        /// 绑定商户列表
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="clbTraderList">控件</param>
        public static void BindTraderList(string fileName, CheckedListBox clbTraderList)
        {
            string xmlFilePath = System.Environment.CurrentDirectory + "/Trader/" + fileName;
            DataTable dt = XmlHelper.ReadXmlNodeByTrader(xmlFilePath, Const.CONST_XPath);
            clbTraderList.DataSource = dt;
            clbTraderList.DisplayMember = "Text";
            clbTraderList.ValueMember = "Value";
            clbTraderList.SelectedIndex = 0;
        }
        #endregion
    }
}
