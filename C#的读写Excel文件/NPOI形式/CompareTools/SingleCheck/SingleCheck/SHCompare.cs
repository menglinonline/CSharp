using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AccountChecking
{
    class SHCompare
    {
        private string[] fileYH;
        private string[] fileST;
        private string savefile;
        //private int platform;//1.PC;2.MOBILE
        //private bool isComputePC = false;
        //private bool isComputeMobile = false;
        private string encodeType;//RD文件编码格式默认
        private int fileSrc;//文件来源，1是全部的那个文件，无后缀，2是银联明细，无后缀
        private decimal merRate;//商户的手续费率
        private List<string> orderFill = new List<string>();

        private int tradeCnt = 0;//交易笔数
        private decimal tradeSum = 0;  //交易总金额 

        private string tradeStartDate = String.Empty;//对账开始时间
        private string tradeEndDate = String.Empty;//对账结束时间
        private string traderCode = "TraderSelect";  //选择商户号：默认选择所有商户 

        /// <param name="fileYH"></param>
        /// <param name="fileST"></param>
        /// <param name="savefile"></param>
        /// <param name="platform"></param>
        /// <param name="fileSrc"></param>
        public SHCompare(IEnumerable<string> fileYH, IEnumerable<string> fileST, string savefile, string traderCode, int fileSrc, decimal merRate, string encodeType = "GB2312")
        {
            this.fileYH = fileYH.ToArray();
            this.fileST = fileST.ToArray();
            this.savefile = savefile;
            this.traderCode = traderCode;
            this.fileSrc = fileSrc;
            this.merRate = merRate * 0.01m;
            this.encodeType = encodeType;
            SetDateRange(fileSrc);
        }

        private void SetDateRange(int fileSrc)
        {
            DateTime dt;
            if (fileSrc == 1)
            {
                IEnumerable<string> order = this.fileYH.OrderBy(m => m.ToLower());
                //int cnt = this.fileYH.Length;
                string date = String.Empty;
                //开始时间 //RD200715100300 => 15100300
                string fileName = order.FirstOrDefault();
                date = fileName.Substring(fileName.LastIndexOf("\\") + 7);
                //DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                //dtFormat.ShortDatePattern = "yyyyMMddHHmmss";
                //dt = Convert.ToDateTime(date, dtFormat);
                dt = DateTime.ParseExact(date, "yyMMddHH", CultureInfo.CurrentCulture);
                this.tradeStartDate = dt.AddHours(-1).ToString("yyyyMMddHHmmss");

                //结束时间 //RD200715100300 => 15100300
                string fileName2 = order.LastOrDefault();
                date = fileName2.Substring(fileName.LastIndexOf("\\") + 7);
                dt = DateTime.ParseExact(date, "yyMMddHH", CultureInfo.CurrentCulture);
                this.tradeEndDate = dt.AddHours(23).ToString("yyyyMMddHHmmss");
            }
            else if (fileSrc == 2)
            {
                IEnumerable<string> order = this.fileYH.OrderBy(m => m.ToLower());
                //int cnt = this.fileYH.Length;
                string date = String.Empty;
                //开始时间 //INN15101288ZM_826520148160010 => 151012
                string fileName = order.FirstOrDefault(); ;
                date = fileName.Substring(fileName.LastIndexOf("\\") + 4, 6);
                dt = DateTime.ParseExact(date, "yyMMdd", CultureInfo.CurrentCulture);
                this.tradeStartDate = dt.AddHours(-1).ToString("yyyyMMddHHmmss");

                //结束时间 //INN15101288ZM_826520148160010 => 151012
                string fileName2 = order.LastOrDefault();
                date = fileName2.Substring(fileName.LastIndexOf("\\") + 4, 6);
                dt = DateTime.ParseExact(date, "yyMMdd", CultureInfo.CurrentCulture);
                this.tradeEndDate = dt.AddHours(23).ToString("yyyyMMddHHmmss");
            }
        }

        public void Compare()
        {
            if (fileYH == null || fileST == null) return;

            DataTable dt = null;

            if (fileSrc == 1)
                GetDataFromRD(out dt);
            else if (fileSrc == 2)
                GetUnionData(out dt);
            else if (fileSrc == 3)
                GetUniteDataFromExcel(out dt);
            else if (fileSrc == 4)
                GetQtopayDataFromExcel(out dt);
            else if (fileSrc == 5)//攒善付
                GetZSFDataFromExcel(out dt);

            if (dt.Rows.Count > 0 && fileST.Length > 0)
            {
                DataTable dtST = GetSTData();
                //银联文件为2, RD/通联为1/3, 中付支付4
                DataTable result = new DataTable();
                if (fileSrc == 2)
                    result = CreateTableContainHeader();
                else if (fileSrc == 4)
                    result = CreateQtopayTableContainHeader();
                else if (fileSrc == 5)//攒善付支付
                    result = CreateZSFPayTableContainHeader();
                else
                    result = CreateTableHeaderFromRD();

                var s1 = from r in dt.AsEnumerable()
                         join r2 in dtST.AsEnumerable() on r.Field<string>("系统交易单号") equals r2.Field<string>("系统交易单号") into dd
                         from d in dd.DefaultIfEmpty()
                         where d == null
                         select r;
                //2银行账单中存在，系统文件中不存在
                foreach (var item in s1)
                {
                    DataRow r = result.NewRow();
                    foreach (DataColumn col in result.Columns)
                    {
                        r[col] = item[col.ColumnName];
                    }
                    r["结果"] = "银联账单中存在，系统中不存在";
                    result.Rows.Add(r);
                }
                //3，银行中不存在，系统中存在
                var s2 = from r2 in dtST.AsEnumerable()
                         join r in dt.AsEnumerable() on r2.Field<string>("系统交易单号") equals r.Field<string>("系统交易单号") into dd
                         from d in dd.DefaultIfEmpty()
                         where d == null
                         select r2;
                foreach (var item in s2)
                {
                    DataRow r = result.NewRow();
                    if (fileSrc == 3)
                        r["系统交易单号"] = item["系统参考号"].ToString();
                    else
                        r["系统交易单号"] = item["系统交易单号"];
                    r["系统参考号"] = item["系统参考号"];
                    r["交易金额"] = item["交易金额"];
                    r["结果"] = "系统账单中存在，银联不存在";
                    r["所属文件"] = item["所属文件"];
                    r["交易时间"] = item["交易时间"];
                    r["交易渠道平台"] = "SYSTEM";
                    result.Rows.Add(r);
                }

                //4，交易金额不匹配
                var s3 = from r in dt.AsEnumerable()
                         join r2 in dtST.AsEnumerable() on r.Field<string>("系统交易单号") equals r2.Field<string>("系统交易单号") into dd
                         from d in dd.DefaultIfEmpty()
                         where d != null && r.Field<decimal>("交易金额") != d.Field<decimal>("交易金额")
                         select new { row1 = r, row2 = d };
                foreach (var item in s3)
                {
                    DataRow r = result.NewRow();
                    foreach (DataColumn col in result.Columns)
                    {
                        r[col] = item.row1[col.ColumnName];
                    }
                    r["交易金额"] = String.Format("银联:{0},系统:{1}", ConvertToString(r["交易金额"]), ConvertToString(item.row2["交易金额"]));
                    r["结果"] = "交易金额不匹配";
                    r["所属文件"] = item.row2["所属文件"];
                    r["交易时间"] = item.row2["交易时间"];
                    r["交易渠道平台"] = item.row2["交易渠道平台"];
                    result.Rows.Add(r);
                }

                try
                {
                    int length = orderFill.Count;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < result.Rows.Count; j++)
                        {
                            DataRow row = result.Rows[j];
                            string order = ConvertToString(row["系统参考号"]);
                            if (order.Equals(orderFill[i]))
                                result.Rows.Remove(row);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                //var table = from ret in result.AsEnumerable()
                //            join order in orderFill.AsEnumerable()
                //            on ret.Field<string>("系统参考号") equals order into fill
                //            from d in fill.DefaultIfEmpty()
                //            where d == null
                //            select ret;
                //ExcelHelper.RenderAsEnumerableToExcel(table, result.Columns, savefile);

                ExcelHelper.RenderDataTableToExcel(result, savefile);

                ISheet oldsheet = ExcelHelper.ReadSheetFromExcelFile(savefile, 0);
                ISheet newsheet = CreateExcelSheetHeader(oldsheet);

                if (fileSrc == 1)
                {
                    CreateSubTotalSheet(dtST, result, newsheet);
                    //创建CP数据明细页
                    CreateSubSheetCP(dtST, result, oldsheet);
                }
                else if (fileSrc == 2)
                    CreateSubTotalUnionSheet(dtST, result, newsheet);
                else if (fileSrc == 3)
                    CreateSubTotalUniteSheet(dtST, result, newsheet);
                else if (fileSrc == 4)
                    CreateSubTotalQtopaySheet(dtST, result, newsheet);
                else if (fileSrc == 5)//攒善付
                    CreateSubTotalZSFPaySheet(dtST, result, newsheet);


                if (fileSrc == 1 || fileSrc == 3)
                    //创建手续费率数据明细页
                    CreateSubSheetRate(dt, oldsheet);

                ExcelHelper.WriteToExcel(newsheet.Workbook, savefile);
            }
            else
            {
                if (fileSrc == 1)
                    MessageBox.Show("RD文件中没有符合条件的数据");
                else if (fileSrc == 2)
                    MessageBox.Show("银联文件中没有符合条件的数据");
                else if (fileSrc == 4)
                    MessageBox.Show("中付支付文件中没有符合条件的数据");
            }
        }

        private static ISheet CreateExcelSheetHeader(ISheet oldsheet)
        {
            oldsheet.DefaultColumnWidth = 11;
            ISheet newsheet = oldsheet.Workbook.CreateSheet();
            ICellStyle style1 = newsheet.Workbook.CreateCellStyle();//样式
            style1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;//文字水平对齐方式
            style1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            newsheet.DefaultColumnWidth = 20;
            IRow row0 = newsheet.CreateRow(newsheet.LastRowNum == 0 ? 0 : newsheet.LastRowNum + 1);
            ICell cellName0 = row0.CreateCell(0);
            ICell cellCount0 = row0.CreateCell(1);
            ICell cellDeposit0 = row0.CreateCell(2);
            ICell cellDetail0 = row0.CreateCell(3);
            ICell cellDetailDeposit0 = row0.CreateCell(4);
            cellCount0.SetCellValue("交易笔数");
            cellDeposit0.SetCellValue("交易金额");
            cellDetail0.SetCellValue("明细差异");
            cellDetailDeposit0.SetCellValue("明细差异金额");
            return newsheet;
        }

        private void CreateSubSheetCP(DataTable dtST, DataTable result, ISheet oldsheet)
        {
            var cpTable = from cp in dtST.AsEnumerable().Where(m => m.Field<string>("交易渠道平台").Contains("CP"))
                          join ret in result.AsEnumerable().Where(p => "SYSTEM".Equals(p.Field<string>("交易渠道平台")))
                            on cp.Field<string>("系统交易单号") equals ret.Field<string>("系统交易单号") into trade
                          from d in trade.DefaultIfEmpty()
                          where d == null
                          select cp;

            var cpGroup = cpTable.ToList().GroupBy(m => m.Field<string>("所属文件"));

            foreach (var item in cpGroup)
            {
                ISheet cpsheet = oldsheet.Workbook.CreateSheet();
                cpsheet.DefaultColumnWidth = 20;
                IRow rowcp = cpsheet.CreateRow(cpsheet.LastRowNum == 0 ? 0 : cpsheet.LastRowNum + 1);
                rowcp.CreateCell(0).SetCellValue("系统交易单号");
                rowcp.CreateCell(1).SetCellValue("系统参考号");
                rowcp.CreateCell(2).SetCellValue("交易金额");
                rowcp.CreateCell(3).SetCellValue("所属文件");
                rowcp.CreateCell(4).SetCellValue("交易渠道平台");

                List<DataRow> cpList = item.ToList();
                int cpCnt = cpList.Count();
                for (int l = 0; l < cpCnt; l++)
                {
                    DataRow data = cpList[l];
                    IRow row = cpsheet.CreateRow(cpsheet.LastRowNum + 1);
                    ICell tradecode = row.CreateCell(0);
                    tradecode.SetCellType(CellType.String);
                    tradecode.SetCellValue(ConvertToString(data["系统交易单号"]));
                    row.CreateCell(1).SetCellValue(ConvertToString(data["系统参考号"]));
                    row.CreateCell(2).SetCellValue(ConvertToString(data["交易金额"]));
                    row.CreateCell(3).SetCellValue(ConvertToString(data["所属文件"]));
                    row.CreateCell(4).SetCellValue(ConvertToString(data["交易渠道平台"]));
                }

            }
        }

        private void CreateSubSheetRate(DataTable dtBank, ISheet oldsheet)
        {
            ICellStyle style = oldsheet.Workbook.CreateCellStyle();
            //NPOI.HSSF.Util.HSSFColor.PALE_BLUE.Index
            style.FillForegroundColor = 44;
            style.FillPattern = FillPattern.SolidForeground;

            var bankGroup = dtBank.AsEnumerable().Where(m => decimal.Round((m.Field<decimal>("交易金额") * merRate), 2) < (m.Field<decimal>("商户费用") * -1));

            int cols = 0; //dtBank.Columns.Count;//表列数
            ISheet bankSheet = oldsheet.Workbook.CreateSheet();
            bankSheet.DefaultColumnWidth = 20;

            IRow rowBank = bankSheet.CreateRow(bankSheet.LastRowNum == 0 ? 0 : bankSheet.LastRowNum + 1);
            foreach (DataColumn dc in dtBank.Columns)
                rowBank.CreateCell(cols++).SetCellValue(dc.ColumnName);

            foreach (var item in bankGroup)
            {
                cols = 0; //dtBank.Columns.Count;//表列数
                IRow row = bankSheet.CreateRow(bankSheet.LastRowNum + 1);
                foreach (DataColumn dc in dtBank.Columns)
                    if ("手续费率".Equals(dc.ColumnName))
                    {
                        ICell cell = row.CreateCell(cols++);
                        cell.SetCellValue(ConvertToString(item[dc.ColumnName]));
                        cell.CellStyle = style;
                    }
                    else
                        row.CreateCell(cols++).SetCellValue(ConvertToString(item[dc.ColumnName]));
            }
        }

        private void CreateSubTotalSheet(DataTable dtST, DataTable result, ISheet newsheet)
        {
            IRow row1 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName1 = row1.CreateCell(0);
            ICell cellCount1 = row1.CreateCell(1);
            ICell cellDeposit1 = row1.CreateCell(2);
            ICell cellCount1_1 = row1.CreateCell(3);
            ICell cellDeposit1_1 = row1.CreateCell(4);
            cellName1.SetCellValue("Mobile/PC总计");
            cellCount1.SetCellValue(tradeCnt);
            cellDeposit1.SetCellValue((double)tradeSum);
            cellCount1_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='RD'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='RD'")));
            cellDeposit1_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='RD'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='RD'")));

            //IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            //ICell cellName2 = row2.CreateCell(0);
            //ICell cellCount2 = row2.CreateCell(1);
            //ICell cellDeposit2 = row2.CreateCell(2);
            //ICell cellCount2_1 = row2.CreateCell(3);
            //ICell cellDeposit2_1 = row2.CreateCell(4);
            //cellName2.SetCellValue("系统总计");
            //cellCount2.SetCellValue(Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'")));
            //cellDeposit2.SetCellValue(Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'")));
            //cellCount2_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'")));
            //cellDeposit2_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'")));
            var group = from r in dtST.AsEnumerable()
                        group r by r.Field<string>("所属文件") into p
                        select p;

            foreach (var item in group)
            {
                IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
                ICell cellName2 = row2.CreateCell(0);
                ICell cellCount2 = row2.CreateCell(1);
                ICell cellDeposit2 = row2.CreateCell(2);
                ICell cellCount2_1 = row2.CreateCell(3);
                ICell cellDeposit2_1 = row2.CreateCell(4);

                cellName2.SetCellValue(item.Key);
                cellCount2.SetCellValue(item.ToList().Count());
                cellDeposit2.SetCellValue(Convert.ToDouble(item.ToList().Sum(m => m.Field<decimal>("交易金额"))));

                var compare = from r in result.AsEnumerable()
                              where "SYSTEM".Equals(r.Field<string>("交易渠道平台"))
                              select r;

                cellCount2_1.SetCellValue(compare.ToList().Where(m => item.Key.Equals(m.Field<string>("所属文件"))).Count());
                cellDeposit2_1.SetCellValue(Convert.ToDouble(compare.ToList().Where(m => item.Key.Equals(m.Field<string>("所属文件"))).Sum(m => m.Field<decimal>("交易金额"))));
            }

            IRow row6 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName6 = row6.CreateCell(0);
            ICell cellCount6 = row6.CreateCell(1);
            ICell cellDeposit6 = row6.CreateCell(2);
            cellName6.SetCellValue("差额");

            int sysCnt = Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'"));
            double sysSum = Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'"));
            cellCount6.SetCellValue(tradeCnt - sysCnt);
            cellDeposit6.SetCellValue((double)tradeSum - sysSum);
        }

        private void CreateSubTotalUniteSheet(DataTable dtST, DataTable result, ISheet newsheet)
        {
            IRow row1 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName1 = row1.CreateCell(0);
            ICell cellCount1 = row1.CreateCell(1);
            ICell cellDeposit1 = row1.CreateCell(2);
            ICell cellCount1_1 = row1.CreateCell(3);
            ICell cellDeposit1_1 = row1.CreateCell(4);
            cellName1.SetCellValue("Mobile/PC总计");
            cellCount1.SetCellValue(tradeCnt);
            cellDeposit1.SetCellValue((double)tradeSum);
            cellCount1_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='UNITE'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='UNITE'")));
            cellDeposit1_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='UNITE'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='UNITE'")));

            IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName2 = row2.CreateCell(0);
            ICell cellCount2 = row2.CreateCell(1);
            ICell cellDeposit2 = row2.CreateCell(2);
            ICell cellCount2_1 = row2.CreateCell(3);
            ICell cellDeposit2_1 = row2.CreateCell(4);
            cellName2.SetCellValue("系统总计");
            cellCount2.SetCellValue(Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'")));
            cellDeposit2.SetCellValue(Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'")));
            cellCount2_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'")));
            cellDeposit2_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'")));

            IRow row6 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName6 = row6.CreateCell(0);
            ICell cellCount6 = row6.CreateCell(1);
            ICell cellDeposit6 = row6.CreateCell(2);
            cellName6.SetCellValue("差额");

            int sysCnt = Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'"));
            double sysSum = Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'"));
            cellCount6.SetCellValue(tradeCnt - sysCnt);
            cellDeposit6.SetCellValue((double)tradeSum - sysSum);
        }

        private void CreateSubTotalUnionSheet(DataTable dtST, DataTable result, ISheet newsheet)
        {
            IRow row1 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName1 = row1.CreateCell(0);
            ICell cellCount1 = row1.CreateCell(1);
            ICell cellDeposit1 = row1.CreateCell(2);
            ICell cellCount1_1 = row1.CreateCell(3);
            ICell cellDeposit1_1 = row1.CreateCell(4);
            cellName1.SetCellValue("Mobile/PC总计");
            cellCount1.SetCellValue(tradeCnt);
            cellDeposit1.SetCellValue((double)tradeSum);
            cellCount1_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='UNOIN'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='UNOIN'")));
            cellDeposit1_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='UNOIN'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='UNOIN'")));

            IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName2 = row2.CreateCell(0);
            ICell cellCount2 = row2.CreateCell(1);
            ICell cellDeposit2 = row2.CreateCell(2);
            ICell cellCount2_1 = row2.CreateCell(3);
            ICell cellDeposit2_1 = row2.CreateCell(4);
            cellName2.SetCellValue("系统总计");
            cellCount2.SetCellValue(Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'")));
            cellDeposit2.SetCellValue(Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'")));
            cellCount2_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'")));
            cellDeposit2_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'")));

            IRow row6 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName6 = row6.CreateCell(0);
            ICell cellCount6 = row6.CreateCell(1);
            ICell cellDeposit6 = row6.CreateCell(2);
            cellName6.SetCellValue("差额");

            int sysCnt = Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'"));
            double sysSum = Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'"));
            cellCount6.SetCellValue(tradeCnt - sysCnt);
            cellDeposit6.SetCellValue((double)tradeSum - sysSum);
        }

        //中付支付结果文件导出
        private void CreateSubTotalQtopaySheet(DataTable dtST, DataTable result, ISheet newsheet)
        {
            IRow row1 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName1 = row1.CreateCell(0);
            ICell cellCount1 = row1.CreateCell(1);
            ICell cellDeposit1 = row1.CreateCell(2);
            ICell cellCount1_1 = row1.CreateCell(3);
            ICell cellDeposit1_1 = row1.CreateCell(4);
            cellName1.SetCellValue("Mobile/PC总计");
            cellCount1.SetCellValue(tradeCnt);
            cellDeposit1.SetCellValue((double)tradeSum);
            cellCount1_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='QTOPAY'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='QTOPAY'")));
            cellDeposit1_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='QTOPAY'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='QTOPAY'")));

            IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName2 = row2.CreateCell(0);
            ICell cellCount2 = row2.CreateCell(1);
            ICell cellDeposit2 = row2.CreateCell(2);
            ICell cellCount2_1 = row2.CreateCell(3);
            ICell cellDeposit2_1 = row2.CreateCell(4);
            cellName2.SetCellValue("系统总计");
            cellCount2.SetCellValue(Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'")));
            cellDeposit2.SetCellValue(Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'")));
            cellCount2_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'")));
            cellDeposit2_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'")));

            IRow row6 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName6 = row6.CreateCell(0);
            ICell cellCount6 = row6.CreateCell(1);
            ICell cellDeposit6 = row6.CreateCell(2);
            cellName6.SetCellValue("差额");

            int sysCnt = Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'"));
            double sysSum = Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'"));
            cellCount6.SetCellValue(tradeCnt - sysCnt);
            cellDeposit6.SetCellValue((double)tradeSum - sysSum);
        }

        //攒善付支付结果文件导出
        private void CreateSubTotalZSFPaySheet(DataTable dtST, DataTable result, ISheet newsheet)
        {
            IRow row1 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName1 = row1.CreateCell(0);
            ICell cellCount1 = row1.CreateCell(1);
            ICell cellDeposit1 = row1.CreateCell(2);
            ICell cellCount1_1 = row1.CreateCell(3);
            ICell cellDeposit1_1 = row1.CreateCell(4);
            cellName1.SetCellValue("Mobile/PC总计");
            cellCount1.SetCellValue(tradeCnt);
            cellDeposit1.SetCellValue((double)tradeSum);
            cellCount1_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='ZSFPAY'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='ZSFPAY'")));
            cellDeposit1_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='ZSFPAY'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='ZSFPAY'")));

            IRow row2 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName2 = row2.CreateCell(0);
            ICell cellCount2 = row2.CreateCell(1);
            ICell cellDeposit2 = row2.CreateCell(2);
            ICell cellCount2_1 = row2.CreateCell(3);
            ICell cellDeposit2_1 = row2.CreateCell(4);
            cellName2.SetCellValue("系统总计");
            cellCount2.SetCellValue(Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'")));
            cellDeposit2.SetCellValue(Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'")));
            cellCount2_1.SetCellValue(Convert.ToInt32(result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("count(交易金额)", "交易渠道平台='SYSTEM'")));
            cellDeposit2_1.SetCellValue((double)Convert.ToDouble(result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'") == DBNull.Value ? 0 : result.Compute("sum(交易金额)", "交易渠道平台='SYSTEM'")));

            IRow row6 = newsheet.CreateRow(newsheet.LastRowNum + 1);
            ICell cellName6 = row6.CreateCell(0);
            ICell cellCount6 = row6.CreateCell(1);
            ICell cellDeposit6 = row6.CreateCell(2);
            cellName6.SetCellValue("差额");

            int sysCnt = Convert.ToInt32(dtST.Compute("count(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("count(交易金额)", "交易渠道平台='SYS'"));
            double sysSum = Convert.ToDouble(dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'") == DBNull.Value ? 0 : dtST.Compute("sum(交易金额)", "交易渠道平台='SYS'"));
            cellCount6.SetCellValue(tradeCnt - sysCnt);
            cellDeposit6.SetCellValue((double)tradeSum - sysSum);
        }


        private void GetDataFromRD(out DataTable dt)
        {
            dt = CreateTableHeaderFromRD();
            //重置笔数及交易总金额
            tradeCnt = 0;
            tradeSum = 0;

            string year = DateTime.Now.Year.ToString();
            string strTenantCode = "";//商户编号
            Regex regDate = new Regex(@"清算日期：\s*(\S{1,})");
            Regex regTenant = new Regex(@"商户编号：\s*(\S{1,})");
            Regex regSubtotal = new Regex(@"小计\s*(\d{1,})\s*(\S{1,})\s*(\S{1,})\s*(\S{1,})");

            bool tarTrader = "TraderSelect".Equals(traderCode) ? true : false;//处理?
            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                bool subtotal = false;//小计处理是否完成                
                FileStream fileStream = System.IO.File.OpenRead(fileName);
                Encoding encoding = "GB2312".Equals(encodeType) ? Encoding.Default : Encoding.UTF8;
                StreamReader streamReader = new System.IO.StreamReader(fileStream, encoding);
                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        string strLine = streamReader.ReadLine();
                        //如果该行为空则跳过此次处理
                        if ("".Equals(strLine.Trim())) continue;

                        Regex reg = new Regex(@"(\d{1,})\s+(\d{1,})\s+(\d{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+(\S{1,})\s+");
                        System.Text.RegularExpressions.Match dataMatch = reg.Match(strLine);
                        if (tarTrader && dataMatch.Success)
                        {
                            DataRow row = CreateRowFromRD(dt, year, dataMatch);
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            //商户编号验证
                            if (!"TraderSelect".Equals(traderCode))
                            {
                                System.Text.RegularExpressions.Match regTenantMatch = regTenant.Match(strLine);
                                if (regTenantMatch.Success)
                                {
                                    strTenantCode = regTenantMatch.Groups[1].Value;
                                    if (strTenantCode.Equals(traderCode))
                                        tarTrader = true;
                                    else tarTrader = false;//默认非处理对象
                                }
                            }

                            System.Text.RegularExpressions.Match regDateMatch = regDate.Match(strLine);
                            if (regDateMatch.Success)
                                year = regDateMatch.Groups[1].Value.Substring(0, 4);
                            else
                            {
                                System.Text.RegularExpressions.Match subtotalMatch = regSubtotal.Match(strLine);
                                if (subtotalMatch.Success)
                                {
                                    //小计处理完成标记
                                    if (tarTrader && !subtotal)
                                    {
                                        subtotal = true;
                                        string subtotalstring = subtotalMatch.Value;
                                        tradeCnt += Convert.ToInt32(subtotalMatch.Groups[1].Value);
                                        tradeSum += Convert.ToDecimal(subtotalMatch.Groups[2].Value);
                                    }
                                    else //如果已处理则跳过·重置
                                        subtotal = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    fileStream.Close();
                    streamReader.Close();
                    MessageBox.Show(e.Message);
                }
            }
        }

        private DataRow CreateRowFromRD(DataTable dt, string year, System.Text.RegularExpressions.Match dataMatch)
        {
            DataRow row = dt.NewRow();
            row["终端编号"] = dataMatch.Groups[1].Value;
            row["交易时间"] = dataMatch.Groups[2].Value;
            row["主账号"] = dataMatch.Groups[3].Value;
            row["发卡行"] = dataMatch.Groups[4].Value;
            decimal tradeMoney = Convert.ToDecimal(dataMatch.Groups[5].Value);
            decimal tradeFee = Convert.ToDecimal(dataMatch.Groups[6].Value);
            row["交易金额"] = tradeMoney;
            row["商户费用"] = tradeFee;
            row["结算金额"] = Convert.ToDecimal(dataMatch.Groups[7].Value);
            row["系统参考号"] = dataMatch.Groups[8].Value;
            row["系统跟踪号"] = dataMatch.Groups[9].Value;
            row["交易渠道"] = dataMatch.Groups[10].Value;
            row["交易类型"] = dataMatch.Groups[11].Value;
            //20160510 手续费率处理添加
            row["手续费率"] = Convert.ToDouble((tradeFee * -1) / tradeMoney).ToString("P4");
            row["交易渠道平台"] = "RD";
            row["系统交易单号"] = string.Format("{0}{1}{2}8", year, dataMatch.Groups[2].Value, dataMatch.Groups[9].Value);
            //1正常，2银行账单存在，系统中不存在，3，银行账单不存在，系统中存在,4，交易金额不匹配
            row["结果"] = "正常";
            return row;
        }

        private static DataTable CreateTableHeaderFromRD()
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("终端编号", typeof(string)));
            dt.Columns.Add(new DataColumn("交易时间", typeof(string)));
            dt.Columns.Add(new DataColumn("主账号", typeof(string)));
            dt.Columns.Add(new DataColumn("发卡行", typeof(string)));
            dt.Columns.Add(new DataColumn("交易金额", typeof(decimal)));//4
            dt.Columns.Add(new DataColumn("商户费用", typeof(decimal)));
            dt.Columns.Add(new DataColumn("结算金额", typeof(decimal)));
            dt.Columns.Add(new DataColumn("系统参考号", typeof(string)));
            dt.Columns.Add(new DataColumn("系统跟踪号", typeof(string)));
            dt.Columns.Add(new DataColumn("交易渠道", typeof(string)));
            dt.Columns.Add(new DataColumn("交易类型", typeof(string)));
            //20160510 手续费率处理添加
            dt.Columns.Add(new DataColumn("手续费率", typeof(string)));
            dt.Columns.Add(new DataColumn("交易渠道平台", typeof(string)));
            dt.Columns.Add(new DataColumn("系统交易单号", typeof(string)));
            dt.Columns.Add(new DataColumn("结果", typeof(string)));
            dt.Columns.Add(new DataColumn("所属文件", typeof(string)));
            return dt;
        }

        private void GetUnionData(out DataTable dt)
        {
            dt = CreateTableContainHeader();
            //重置笔数及交易总金额
            tradeCnt = 0;
            tradeSum = 0;

            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                FileStream fileStream = System.IO.File.OpenRead(fileName);
                StreamReader streamReader = new System.IO.StreamReader(fileStream, Encoding.Default);
                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        string lineDataStr = streamReader.ReadLine();

                        Regex reg = new Regex(@"(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+\s+)+");
                        System.Text.RegularExpressions.Match dataMatch = reg.Match(lineDataStr);
                        if (dataMatch.Success)
                        {
                            DataRow row = dt.NewRow();
                            row["列1"] = dataMatch.Groups[1].Value;
                            row["列2"] = dataMatch.Groups[2].Value;
                            row["列3"] = dataMatch.Groups[3].Value;
                            row["列4"] = dataMatch.Groups[4].Value;
                            row["列5"] = dataMatch.Groups[5].Value;
                            row["列6"] = dataMatch.Groups[6].Value;
                            row["交易金额"] = Convert.ToDecimal(dataMatch.Groups[7].Value) / 100;
                            row["列8"] = dataMatch.Groups[8].Value;
                            row["列9"] = dataMatch.Groups[9].Value;
                            row["系统交易单号"] = dataMatch.Groups[10].Value;
                            row["交易时间"] = dataMatch.Groups[11].Value;
                            row["系统参考号"] = dataMatch.Groups[12].Value;
                            row["交易渠道平台"] = "UNOIN";
                            row["结果"] = "正常";
                            dt.Rows.Add(row);
                        }
                    }

                    tradeCnt = dt.Rows.Count;
                    tradeSum = Convert.ToDecimal(dt.Compute("sum(交易金额)", ""));
                }
                catch (Exception e)
                {
                    fileStream.Close();
                    streamReader.Close();
                    MessageBox.Show(e.Message);
                }
            }


        }

        private void GetUniteDataFromExcel(out DataTable dt)
        {
            dt = CreateTableHeaderFromRD();
            //重置笔数及交易总金额
            tradeCnt = 0;
            tradeSum = 0;

            int year = DateTime.Now.Year;
            Regex subTotalRegular = new Regex(@"交易笔数：(\d{1,})\s*交易本金：(\d{1,}(?:[\.]\d{1,2}|\d{1,2}))\s*交易手续费：(?:-*)(\d{1,}(?:[\.]\d{1,2}|\d{1,2}))\s*分期手续费：(\d{1,}(?:[\.]\d{1,2}|\d{1,2}))\s*");

            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                ISheet sheet = ExcelHelper.ReadSheetFromExcelFile(fileName, 0);
                if (sheet == null) { MessageBox.Show("该文件正在被另一个进程使用,请关闭Excel后重新执行"); break; }

                IRow excel = null;
                try
                {
                    //读取第三行的数据 获取来源类型PC/MOBILE 及 年份
                    excel = sheet.GetRow(2);  //读取当前行数据
                    string tenantCode = excel.GetCell(2).StringCellValue;
                    string yearCode = excel.GetCell(10).StringCellValue;
                    year = Convert.ToInt32(yearCode.Substring(0, 4));

                    for (int p = 10; p < sheet.LastRowNum - 3; p++)
                    {
                        excel = sheet.GetRow(p);  //读取当前行数据
                        if ("小计".Equals(excel.GetCell(1).StringCellValue)) continue;

                        DataRow row = dt.NewRow();
                        row["终端编号"] = excel.GetCell(0).StringCellValue;
                        row["交易时间"] = excel.GetCell(1).StringCellValue;
                        row["交易类型"] = excel.GetCell(2).StringCellValue;
                        row["主账号"] = excel.GetCell(5).StringCellValue;
                        row["发卡行"] = excel.GetCell(8).StringCellValue;
                        decimal tradeMoney = Convert.ToDecimal(excel.GetCell(11).StringCellValue);
                        decimal tradeFee = Convert.ToDecimal(excel.GetCell(12).StringCellValue);
                        row["交易金额"] = tradeMoney;
                        row["商户费用"] = tradeFee;
                        row["结算金额"] = Convert.ToDecimal(excel.GetCell(11).StringCellValue);
                        row["系统参考号"] = String.Format("{0}{1}", year, excel.GetCell(1).StringCellValue);
                        row["系统跟踪号"] = excel.GetCell(15).StringCellValue;
                        row["交易渠道"] = "通联平台";
                        //20160510 手续费率处理添加
                        row["手续费率"] = Convert.ToDouble((tradeFee * -1) / tradeMoney).ToString("P4");
                        row["交易渠道平台"] = "UNITE";
                        //通联商户文件对比特殊处理
                        if (fileSrc == 3)
                            row["系统交易单号"] = String.Format("{0}{1}", excel.GetCell(1).StringCellValue, excel.GetCell(11).StringCellValue);
                        else
                            row["系统交易单号"] = string.Format("{0}{1}{2}8", year, excel.GetCell(1).StringCellValue, excel.GetCell(3).StringCellValue);
                        //1正常，2银行账单中存在，系统文件中不存在，3，银行中不存在，系统中存在,4，交易金额不匹配
                        row["结果"] = "正常";
                        dt.Rows.Add(row);
                    }

                    //交易笔数：4135  交易本金：1708493.28   交易手续费：-6833.91  分期手续费：0.00  净额：1701659.37    
                    excel = sheet.GetRow(sheet.LastRowNum - 1);  //读取当前行数据
                    System.Text.RegularExpressions.Match subtotal = subTotalRegular.Match(excel.GetCell(1).StringCellValue);
                    if (subtotal.Success)
                    {
                        if (!String.Empty.Equals(subtotal.Value))
                        {
                            tradeCnt += Convert.ToInt32(subtotal.Groups[1].Value);
                            tradeSum += Convert.ToDecimal(subtotal.Groups[2].Value);
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void GetQtopayDataFromExcel(out DataTable dt)
        {
            dt = CreateQtopayTableContainHeader();
            //重置笔数及交易总金额
            tradeCnt = 0;
            tradeSum = 0;

            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                ISheet sheet = ExcelHelper.ReadSheetFromExcelFile(fileName, 0);
                if (sheet == null) { MessageBox.Show("该文件正在被另一个进程使用,请关闭Excel后重新执行"); break; }

                IRow excel = null;
                try
                {
                    //读取第三行的数据 获取商户号
                    excel = sheet.GetRow(2); //读取当前行数据
                    //商户号:847001251370001
                    string strTenant = excel.GetCell(0).StringCellValue;
                    string tenantCode = strTenant.Replace("商户号:", "").Trim();
                    if (!"TraderSelect".Equals(traderCode))
                        if (!traderCode.Equals(tenantCode))
                            continue;

                    for (int p = 4; p < sheet.LastRowNum; p++)
                    {
                        excel = sheet.GetRow(p);  //读取当前行数据

                        DataRow row = dt.NewRow();
                        row["订单号"] = excel.GetCell(0).StringCellValue;//订单号
                        row["交易卡号"] = excel.GetCell(1) == null ? "" : excel.GetCell(1).StringCellValue; //交易卡号
                        row["交易时间"] = excel.GetCell(2).StringCellValue;//交易时间
                        row["交易类型"] = excel.GetCell(3).StringCellValue;//交易类型
                        row["系统交易单号"] = excel.GetCell(4).StringCellValue;//流水号
                        row["交易币种"] = excel.GetCell(5).StringCellValue;//交易币种 
                        row["交易金额"] = Convert.ToDecimal(excel.GetCell(6).NumericCellValue);//交易金额
                        row["币种汇率"] = excel.GetCell(7).StringCellValue;//币种汇率                      
                        row["转换币种"] = excel.GetCell(8).StringCellValue;//转换币种
                        row["转换金额"] = Convert.ToDecimal(excel.GetCell(9).NumericCellValue);//转换金额  
                        //row["系统参考号"] = "";
                        row["交易渠道平台"] = "QTOPAY";//中付支付
                        row["结果"] = "正常";

                        dt.Rows.Add(row);
                    }

                    //合计 交易笔数#	8448(交易笔数)  1642016.31(交易总金额) 9034.45(交易扣费) 1632982.03(结算金额)
                    excel = sheet.GetRow(sheet.LastRowNum);  //读取当前行数据
                    tradeCnt += Convert.ToInt32(excel.GetCell(3).StringCellValue);
                    tradeSum += Convert.ToDecimal(excel.GetCell(6).NumericCellValue);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void GetZSFDataFromExcel(out DataTable dt)
        {
            dt = CreateZSFPayTableContainHeader();
            //重置笔数及交易总金额
            tradeCnt = 0;
            tradeSum = 0;

            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                ISheet sheet = ExcelHelper.ReadSheetFromExcelFile(fileName, 0);
                if (sheet == null) { MessageBox.Show("该文件正在被另一个进程使用,请关闭Excel后重新执行"); break; }

                IRow excel = null;
                try
                {
                    //从第六行开始读取数据
                    for (int p = 5; p < sheet.LastRowNum - 4; p++)
                    {
                        excel = sheet.GetRow(p);  //读取当前行数据

                        DataRow row = dt.NewRow();

                        tradeCnt = tradeCnt + 1;//交易笔数
                        object money = ValueByCellType(excel.GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        tradeSum += Convert.ToDecimal(money);//交易总金额

                        //攒善付™ 订单ID
                        row["系统交易单号"] = ValueByCellType(excel.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK));                       
                        row["交易时间"] = ValueByCellType(excel.GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        row["交易金额"] = money;
                        row["实际支付金额"] = ValueByCellType(excel.GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        row["费率"] = ValueByCellType(excel.GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        row["扣费"] = ValueByCellType(excel.GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        row["扣费后剩余"] = ValueByCellType(excel.GetCell(6, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        row["交易类型"] = ValueByCellType(excel.GetCell(8, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        //额外信息[商户]
                        row["系统参考号"] = ValueByCellType(excel.GetCell(9, MissingCellPolicy.CREATE_NULL_AS_BLANK)); //预留                      
                        row["交易渠道平台"] = "ZSFPAY";//中付支付
                        row["结果"] = "正常";
                        row["所属文件"] = fileName;
                        dt.Rows.Add(row);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private static DataTable CreateTableContainHeader()
        {
            DataTable temp = new DataTable();
            temp.Columns.Add(new DataColumn("列1", typeof(string)));
            temp.Columns.Add(new DataColumn("列2", typeof(string)));
            temp.Columns.Add(new DataColumn("列3", typeof(string)));
            temp.Columns.Add(new DataColumn("列4", typeof(string)));
            temp.Columns.Add(new DataColumn("列5", typeof(string)));
            temp.Columns.Add(new DataColumn("列6", typeof(string)));
            temp.Columns.Add(new DataColumn("交易金额", typeof(decimal)));
            temp.Columns.Add(new DataColumn("列8", typeof(string)));
            temp.Columns.Add(new DataColumn("列9", typeof(string)));//
            temp.Columns.Add(new DataColumn("系统交易单号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易时间", typeof(string)));
            temp.Columns.Add(new DataColumn("系统参考号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易渠道平台", typeof(string)));
            temp.Columns.Add(new DataColumn("结果", typeof(string)));
            temp.Columns.Add(new DataColumn("所属文件", typeof(string)));
            return temp;
        }

        //创建中付支付结果表结构
        private static DataTable CreateQtopayTableContainHeader()
        {
            DataTable temp = new DataTable();
            temp.Columns.Add(new DataColumn("订单号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易卡号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易时间", typeof(string)));
            temp.Columns.Add(new DataColumn("交易类型", typeof(string)));
            temp.Columns.Add(new DataColumn("系统交易单号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易币种", typeof(string)));
            temp.Columns.Add(new DataColumn("交易金额", typeof(decimal)));
            temp.Columns.Add(new DataColumn("币种汇率", typeof(string)));
            temp.Columns.Add(new DataColumn("转换币种", typeof(string)));
            temp.Columns.Add(new DataColumn("转换金额", typeof(decimal)));
            temp.Columns.Add(new DataColumn("系统参考号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易渠道平台", typeof(string)));
            temp.Columns.Add(new DataColumn("结果", typeof(string)));
            temp.Columns.Add(new DataColumn("所属文件", typeof(string)));
            return temp;
        }

        //创建攒善付结果表结构
        private static DataTable CreateZSFPayTableContainHeader()
        {
            DataTable temp = new DataTable();
            //攒善付™ 订单ID	交易时间	订单金额(单位:元)	实际支付金额(单位:元)	费率(%)	扣费(单位:元)	扣费后剩余(单位:元)	支付状态	交易渠道	额外信息[商户]
            temp.Columns.Add(new DataColumn("系统交易单号", typeof(string)));
            temp.Columns.Add(new DataColumn("交易时间", typeof(string)));
            temp.Columns.Add(new DataColumn("交易金额", typeof(decimal)));//订单金额
            temp.Columns.Add(new DataColumn("实际支付金额", typeof(decimal)));//实际支付金额
            temp.Columns.Add(new DataColumn("费率", typeof(decimal)));
            temp.Columns.Add(new DataColumn("扣费", typeof(decimal)));
            temp.Columns.Add(new DataColumn("扣费后剩余", typeof(decimal)));
            temp.Columns.Add(new DataColumn("交易类型", typeof(string)));//交易渠道
            temp.Columns.Add(new DataColumn("系统参考号", typeof(string)));//额外信息[商户]
            temp.Columns.Add(new DataColumn("交易渠道平台", typeof(string)));
            temp.Columns.Add(new DataColumn("结果", typeof(string)));
            temp.Columns.Add(new DataColumn("所属文件", typeof(string)));
            return temp;
        }

        private DataTable GetSTData()
        {
            DataTable dtST = new DataTable();
            dtST.Columns.Add(new DataColumn("系统交易单号", typeof(string)));            
            dtST.Columns.Add(new DataColumn("系统参考号", typeof(string)));//系统参考号       
            dtST.Columns.Add(new DataColumn("交易金额", typeof(decimal)));
            dtST.Columns.Add(new DataColumn("所属文件", typeof(string)));
            dtST.Columns.Add(new DataColumn("交易渠道平台", typeof(string)));
            dtST.Columns.Add(new DataColumn("交易时间", typeof(string)));
            Regex regNo = new Regex(@"(?<=/P：)\d{1,}");
            Regex regZsfNo = new Regex(@"(?<=/P：)\w{1,}");
            Regex regRefer = new Regex(@"(?<=M:)\d{1,}");
            for (int i = 0; i < fileST.Length; i++)
            {
                ISheet sheet = ExcelHelper.ReadSheetFromExcelFile(fileST[i].ToString(), 0);
                if (sheet == null) { MessageBox.Show("该文件正在被另一个进程使用,请关闭Excel后重新执行"); break; }

                IRow row = null;
                string filename = fileST[i].ToString();
                string fileIndex = filename.Substring(filename.LastIndexOf('\\') + 1);
                bool isSpecial = "826520148160010".Equals(traderCode);//隆鑫安泰特殊处理
                int loopCnt = isSpecial || fileIndex.ToUpper().Contains("CP") ? sheet.LastRowNum + 1 : sheet.LastRowNum;
                for (int p = 2; p < loopCnt; p++)
                {
                    row = sheet.GetRow(p);  //读取当前行数据
                    if (row != null)
                    {
                        ICell cellNo = row.GetCell(4);//20151003235740 0625998
                        if (cellNo == null) { continue; }
                        //系统交易单号
                        string sysTradeCode = string.Empty;
                        if (fileSrc == 5)
                            sysTradeCode = regZsfNo.Match(cellNo.StringCellValue).Value;
                        else
                            sysTradeCode = regNo.Match(cellNo.StringCellValue).Value;
                        //RD文件单独商户对账时 时间范围设置前天230000-最后一天225959
                        if (isSpecial && !String.IsNullOrEmpty(sysTradeCode) && (fileSrc == 1 || fileSrc == 2))
                        {
                            string date = sysTradeCode.Substring(0, 14);
                            if (String.Compare(tradeStartDate, date) > 0
                                || String.Compare(tradeEndDate, date) <= 0)
                                continue;
                        }

                        ICell cellPlatform = row.GetCell(6);
                        ICell cellDeposit = row.GetCell(7);
                        ICell cellMerchantFee = row.GetCell(8);
                        ICell cellSystemFee = row.GetCell(9);
                        ICell cellSettle = row.GetCell(10);

                        DataRow rowdtST = dtST.NewRow();

                        rowdtST[0] = sysTradeCode;
                        if (String.IsNullOrEmpty(sysTradeCode))
                        {
                            //系统参考号 //通联商户文件对比特殊处理 20160505
                            if (fileSrc == 3)
                                rowdtST[1] = regRefer.Match(cellNo.StringCellValue).Value;
                            else
                            {
                                string temp = regRefer.Match(cellNo.StringCellValue).Value;
                                temp = String.Format("5{0}", temp.Trim());
                                rowdtST[1] = temp;
                                orderFill.Add(temp);
                            }
                        }
                        else
                        {
                            //通联商户文件对比特殊处理
                            if (fileSrc == 3)
                            {
                                rowdtST[0] = sysTradeCode.Substring(4, 10) + cellDeposit.StringCellValue;
                                rowdtST[1] = sysTradeCode;
                            }
                        }
                        rowdtST[2] = Convert.ToDecimal(cellDeposit.StringCellValue);
                        rowdtST[3] = fileIndex;
                        //1"PC" : 2"MOBILE";
                        if (fileSrc == 1)
                            rowdtST[4] = fileIndex.ToUpper().Contains("CP") ? "CP" : "SYS";
                        else if (fileSrc == 3)
                            rowdtST[4] = "PC".Equals(cellPlatform.StringCellValue) ? "PC" : "MOBILE";
                        else
                            rowdtST[4] = "SYS";//fileSrc == 2 || 4 || 5)

                        //交易时间 20160520                        
                        rowdtST[5] = ValueByCellType(row.GetCell(0,MissingCellPolicy.CREATE_NULL_AS_BLANK));
                        dtST.Rows.Add(rowdtST);
                    }
                }
            }
            return dtST;
        }

        private int ConvertToInt32(object value)
        {

            if (!DBNull.Value.Equals(value))
                return Convert.ToInt32(value);
            else
                return default(int);
        }

        private byte ConvertToByte(object value)
        {

            if (!DBNull.Value.Equals(value))
                return Convert.ToByte(value);
            else
                return default(byte);
        }

        private string ConvertToString(object value)
        {

            if (!DBNull.Value.Equals(value))
                return Convert.ToString(value);
            else
                return "";
        }

        //根据单元格的类型获取值域
        private static object ValueByCellType(ICell cell)
        {
            object result;
            if (cell.CellType == CellType.String)
                result = cell.StringCellValue;
            else if (cell.CellType == CellType.Numeric)
                result = cell.NumericCellValue;
            else if (cell.CellType == CellType.Boolean)
                result = cell.BooleanCellValue;
            else if (cell.CellType == CellType.Error)
                result = cell.ErrorCellValue;
            else
                result = cell.DateCellValue;
            return result;
        }

    }
}
