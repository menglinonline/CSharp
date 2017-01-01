using CompareTools.Common;
using CompareTools.Common.Enumeration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareTools.Business
{
    public class Utility
    {
        #region OleDB形式
        /// <summary>
        /// 读取Excel文件填充到DataSet
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">Excel中的表明</param>
        /// <returns></returns>
        public static DataSet ExcelToDS(string filePath, string sheetName)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                string strExcel = "";
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                try
                {
                    //打开Excel文件并填充数据到DataSet
                    conn.Open();
                    OleDbDataAdapter adp = null;
                    DataSet ds = null;
                    strExcel = "select * from [" + sheetName + "$]";
                    adp = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    adp.Fill(ds);

                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    //关闭Excel数据连接
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }

            }
           
            return null;
        }

        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="outputFolderPath">输出目录路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="settleDate">结算日期</param>
        /// <param name="transMoney">交易本金</param>
        /// <param name="free">手续费</param>
        //public static bool CreateExcelFile(string outputFolderPath, string fileName, string settleDate, string transMoney,string free)
        //{
        //    bool isSuccessed = false;
        //    //创建文件夹
        //    if (!Directory.Exists(outputFolderPath))
        //    {
        //        Directory.CreateDirectory(outputFolderPath);
        //    }
        //    //文件路径
        //    string excelPath = outputFolderPath + "\\" + fileName;
        //    //删除已经存在的文件
        //    if (File.Exists(excelPath))
        //    {
        //        File.Delete(excelPath);
        //    }

        //    if (!File.Exists(excelPath))
        //    {
        //        try
        //        {
        //            //创建文件
        //            FileStream fs = new FileStream(excelPath, FileMode.OpenOrCreate);
        //            fs.SetLength(0);
        //            fs.Write(Properties.Resources.Excel635353311441737186, 0, Properties.Resources.Excel635353311441737186.Length);
        //            fs.Close();
        //            fs.Dispose();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            excelPath = string.Empty;

        //            return isSuccessed;
        //        }
        //        //给Excel文件添加"Everyone,Users"用户组的完全控制权限
        //        FileInfo fi = new FileInfo(excelPath);
        //        System.Security.AccessControl.FileSecurity fileSecurity = fi.GetAccessControl();
        //        fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
        //        fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
        //        fi.SetAccessControl(fileSecurity);

        //        //给Excel文件所在目录添加"Everyone,Users"用户组的完全控制权限
        //        DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(excelPath));
        //        System.Security.AccessControl.DirectorySecurity dirSecurity = di.GetAccessControl();
        //        dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
        //        dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
        //        di.SetAccessControl(dirSecurity);

        //        //定义OleDB连接字符串
        //        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "data source=" + @excelPath + ";" + "Extended Properties=Excel 8.0;";
        //        OleDbConnection conn = new OleDbConnection();
        //        conn.ConnectionString = strConn;
        //        try
        //        {
        //            //打开创建的Excel文件并写入值
        //            conn.Open();
        //            OleDbCommand cmd = null;
        //            //向"Sheet1"表中插入几条数据,访问Excel的表的时候需要在表名后添加"$"符号,Insert语句可以不指定列名
        //            cmd = new OleDbCommand("Insert Into [Sheet1$] Values('" + settleDate + "', '" + transMoney + "', '" + free + "')", conn);//(A,B,C) 
        //            cmd.ExecuteNonQuery();
        //            isSuccessed = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return isSuccessed;
        //        }
        //        finally
        //        {
        //            //关闭Excel数据连接
        //            if (conn.State != ConnectionState.Closed)
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }

        //    return isSuccessed;
        //}
        #endregion

        #region 创建Excel《通联对账单》和《银联RD》
        /// <summary>
        /// 创建工作薄的工作表《通联对账单》和《银联RD》
        /// </summary>
        /// <param name="excelType">Excel文件类型</param>
        /// <param name="outputFilePath">输出文件路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="outputData">导出的数据</param>
        /// <returns></returns>
        public static bool CreateExcel(ExcelFileType excelType, string outputFilePath,
                                        HSSFWorkbook wk, string sheetName, 
                                        KeyValuePair<string, List<string>> outputData)
        {
            bool isSuccessed = false;
            ISheet existSheet = wk.GetSheet(sheetName);
            if (existSheet == null || (existSheet != null && existSheet.SheetName != sheetName))
            {
                ISheet sheet = wk.CreateSheet(sheetName);
                //创建标题行
                IRow row0 = sheet.CreateRow(0);
                row0.Height = 20 * 20;
                //创建标题列
                ICell row0_cell0 = row0.CreateCell(0);
                ICell row0_cell1 = row0.CreateCell(1);
                ICell row0_cell2 = row0.CreateCell(2);
                //创建标题列样式
                row0_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
                row0_cell1.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
                row0_cell2.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);

                //创建标题列内容,《通联对账单》和《银联RD》列名一样
                row0_cell0.SetCellValue("结算日期");
                row0_cell1.SetCellValue("交易本金");
                row0_cell2.SetCellValue("手续费");

                //《银联RD》有笔数列
                if (excelType == ExcelFileType.银联RD)
                {
                    ICell row0_cell3 = row0.CreateCell(3);
                    row0_cell3.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
                    row0_cell3.SetCellValue("笔数");
                }

                //创建内容列
                int rowIndex = 1;
                foreach (string value in outputData.Value)
                {
                    string[] valueArr = value.Split('|');
                    IRow row = sheet.CreateRow(rowIndex);
                    row.Height = 20 * 20;
                    ICell row_cell0 = row.CreateCell(0);
                    ICell row_cell1 = row.CreateCell(1);
                    ICell row_cell2 = row.CreateCell(2);
                    if (excelType == ExcelFileType.通联对账单)
                    {
                        if (valueArr.Length == 3)
                        {
                            row_cell0.SetCellValue(valueArr[0]);
                            row_cell1.SetCellValue(valueArr[1]);
                            row_cell2.SetCellValue(valueArr[2]);
                        }
                    }
                    else if (excelType == ExcelFileType.银联RD)
                    {
                        if (valueArr.Length == 4)
                        {
                            ICell row_cell3 = row.CreateCell(3);

                            row_cell0.SetCellValue(valueArr[0]);
                            row_cell1.SetCellValue(valueArr[1]);
                            row_cell2.SetCellValue(valueArr[2]);
                            row_cell3.SetCellValue(valueArr[3]);
                        }
                    }
                    rowIndex++;
                }

                try
                {
                    using (FileStream stm = File.OpenWrite(outputFilePath))
                    {
                        wk.Write(stm);
                        isSuccessed = true;
                    }
                }
                catch (Exception ex)
                {
                    isSuccessed = false;
                }
            }

            return isSuccessed;
        }
        #endregion

        #region 创建Excel的工作表《日日结&随心付》《银联RD信用卡》
        /// <summary>
        /// 创建工作薄的工作表《日日结&随心付》《银联RD信用卡》
        /// </summary>
        /// <param name="excelType">Excel文件类型</param>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="outputData">导出的数据</param>
        /// <returns></returns>
        public static Dictionary<string, bool> CreateExcel(ExcelFileType excelType, string outputFilePath, 
                                                           HSSFWorkbook wk,string sheetName, 
                                                           Dictionary<string, List<string>> outputData)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            ISheet sheet = wk.CreateSheet(sheetName);
            //创建标题行
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;

            //创建标题列
            ICell row0_cell0 = row0.CreateCell(0);
            ICell row0_cell1 = row0.CreateCell(1);
            ICell row0_cell2 = row0.CreateCell(2);
            ICell row0_cell3 = row0.CreateCell(3);

            //创建标题列样式
            row0_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell1.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell2.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell3.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);

            //创建标题列内容
            if (excelType == ExcelFileType.日日结随心付)
            {
                row0_cell0.SetCellValue("创建日期");
                row0_cell1.SetCellValue("出账金额");
                row0_cell2.SetCellValue("服务费");
                row0_cell3.SetCellValue("笔数");
            }
            else if (excelType == ExcelFileType.银联RD信用卡)
            {
                ICell row0_cell4 = row0.CreateCell(4);
                ICell row0_cell5 = row0.CreateCell(5);
                row0_cell4.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
                row0_cell5.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);

                row0_cell0.SetCellValue("结算日期");
                row0_cell1.SetCellValue("交易金额");
                row0_cell2.SetCellValue("商户费用");
                row0_cell3.SetCellValue("结算金额");
                row0_cell4.SetCellValue("笔数");
            }
           
            //创建内容列
            int rowIndex = 1;
            foreach (KeyValuePair<string, List<string>> data in outputData)
            {
                foreach (string value in data.Value)
                {
                    string[] valueArr = value.Split('|');
                    IRow row = sheet.CreateRow(rowIndex);
                    row.Height = 20 * 20;
                    ICell row_cell0 = row.CreateCell(0);
                    ICell row_cell1 = row.CreateCell(1);
                    ICell row_cell2 = row.CreateCell(2);
                    ICell row_cell3 = row.CreateCell(3);
                    if (excelType == ExcelFileType.日日结随心付)
                    {
                        if (valueArr.Length == 3)
                        {
                            row_cell0.SetCellValue(data.Key);
                            row_cell1.SetCellValue(valueArr[0]);
                            row_cell2.SetCellValue(valueArr[1]);
                            row_cell3.SetCellValue(valueArr[2]);

                            result.Add(data.Key, true);
                        }
                        else
                        {
                            result.Add(data.Key, false);
                        }
                    }
                    else if (excelType == ExcelFileType.银联RD信用卡)
                    {
                        if (valueArr.Length == 4)
                        {
                            ICell row_cell4 = row.CreateCell(4);

                            row_cell0.SetCellValue(data.Key);
                            row_cell1.SetCellValue(valueArr[0]);
                            row_cell2.SetCellValue(valueArr[1]);
                            row_cell3.SetCellValue(valueArr[2]);
                            row_cell4.SetCellValue(valueArr[3]);

                            result.Add(data.Key, true);
                        }
                        else
                        {
                            result.Add(data.Key, false);
                        }
                    }
                }
                rowIndex++;
            }

            //写入文件
            try
            {
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
        #endregion

        #region 创建Excel的工作表《通联支付信用卡》
        /// <summary>
        /// 创建工作薄的工作表《通联支付信用卡》
        /// </summary>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="dt">导出的数据</param>
        /// <returns></returns>
        public static bool CreateExcelSheet(string outputFilePath, HSSFWorkbook wk, string sheetName, DataTable dt)
        {
            bool isSuccessed = false;
            ISheet sheet = wk.CreateSheet(sheetName);

            //创建标题列
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;
            ICell row0_cell0 = row0.CreateCell(0);
            ICell row0_cell1 = row0.CreateCell(1);
            ICell row0_cell2 = row0.CreateCell(2);
            ICell row0_cell3 = row0.CreateCell(3);
            ICell row0_cell4 = row0.CreateCell(4);
            ICell row0_cell5 = row0.CreateCell(5);
            ICell row0_cell6 = row0.CreateCell(6);
            ICell row0_cell7 = row0.CreateCell(7);
            ICell row0_cell8 = row0.CreateCell(8);
            ICell row0_cell9 = row0.CreateCell(9);
            ICell row0_cell10 = row0.CreateCell(10);
            ICell row0_cell11 = row0.CreateCell(11);

            row0_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell1.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell2.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell3.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell4.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell5.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell6.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell7.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell8.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell9.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell10.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell11.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);


            row0_cell0.SetCellValue("交易时间");
            row0_cell1.SetCellValue("交易类型");
            row0_cell2.SetCellValue("凭证号");
            row0_cell3.SetCellValue("卡号");
            row0_cell4.SetCellValue("卡类别");
            row0_cell5.SetCellValue("发卡行代码");
            row0_cell6.SetCellValue("发卡行名称");
            row0_cell7.SetCellValue("交易本金");
            row0_cell8.SetCellValue("手续费");
            row0_cell9.SetCellValue("分期手续费");
            row0_cell10.SetCellValue("原交易日期");
            row0_cell11.SetCellValue("流水号");

            //创建内容列
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow excelRow = sheet.CreateRow(i + 1);
                DataRow valueRow = dt.Rows[i];
                for (int j = 0; j <  dt.Columns.Count; j++)
                {
                    ICell cell = excelRow.CreateCell(j);
                    cell.SetCellValue(valueRow[j].ToString());
                }
            }
            //写入文件
            try
            {
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                    isSuccessed = true;
                }
            }
            catch (Exception ex)
            {
                isSuccessed = false;
            }
          
            return isSuccessed;
        }

        /// <summary>
        /// 创建工作薄的工作表合计表《通联支付信用卡》
        /// </summary>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="totalTransMoney">统计交易本金</param>
        /// <param name="totalTransFree">统计手续费</param>
        /// <param name="totalCount">统计笔数</param>
        /// <returns></returns>
        public static bool CreateExcelSheet(string outputFilePath, HSSFWorkbook wk, string sheetName,
                                            decimal totalTransMoney, decimal totalTransFree, int totalCount)
        {
            bool isSuccessed = false;
            ISheet sheet = wk.CreateSheet(sheetName);
            //创建标题列
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;
            ICell row0_cell0 = row0.CreateCell(0);
            ICell row0_cell1 = row0.CreateCell(1);
            ICell row0_cell2 = row0.CreateCell(2);

            row0_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell1.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell2.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);

            row0_cell0.SetCellValue("交易本金");
            row0_cell1.SetCellValue("手续费");
            row0_cell2.SetCellValue("笔数");

            //创建内容列
            IRow row1 = sheet.CreateRow(1);
            row1.Height = 20 * 20;
            ICell row1_cell0 = row1.CreateCell(0);
            ICell row1_cell1 = row1.CreateCell(1);
            ICell row1_cell2 = row1.CreateCell(2);
            row1_cell0.SetCellValue(totalTransMoney.ToString());
            row1_cell1.SetCellValue(totalTransFree.ToString());
            row1_cell2.SetCellValue(totalCount.ToString());
            try
            {
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                    isSuccessed = true;
                }
            }
            catch (Exception ex)
            {
                isSuccessed = false;
            }

            return isSuccessed;
        }

        /// <summary>
        /// 创建工作表的新行《通联支付信用卡》
        /// </summary>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="dt">导出的数据</param>
        /// <returns></returns>
        public static bool CreateSheetRow(string outputFilePath, HSSFWorkbook wk, string sheetName, DataTable dt)
        {
            bool isSuccessed = false;
            decimal previousSumTransMoney = 0M;//得到原有数据的最后一行合计的交易本金
            decimal previousSumTransFree = 0M; //得到原有数据的最后一行合计的交易手续费
            Int16 previousSumCount = 0; //得到原有数据的最后一行合计的交易笔数
            decimal currSumTransMoney = 0M;//当前数据的最后一行合计的交易本金
            decimal currSumTransFree = 0M;//当前数据的最后一行合计的手续费
            Int16 currSumCount = 0; //当前数据的最后一行合计的交易笔数

            ISheet sheet = wk.GetSheet(sheetName);
            //得到原有数据的最后一行行号
            int previousLastRowNum = sheet.LastRowNum + 1;
            IRow row = sheet.GetRow(sheet.LastRowNum);//读取原有数据的最后一行数据
            if (row != null)
            {
                //为了覆盖掉每个的大合计，最后只有一个大合计
                if (row.GetCell(0).ToString() == "大合计")
                {
                    previousLastRowNum = sheet.LastRowNum;
                }
                ICell cellMoney = row.GetCell(7);//读取交易本金列数据
                if (cellMoney != null)
                {
                    Decimal.TryParse(cellMoney.ToString(), out previousSumTransMoney);
                }
                ICell cellFree = row.GetCell(8);//读取手续费列数据
                if (cellFree != null)
                {
                    Decimal.TryParse(cellFree.ToString(), out previousSumTransFree);
                }
                ICell cellCount = row.GetCell(9);//读取手续费列数据
                if (cellCount != null)
                {
                    Int16.TryParse(cellCount.ToString(), out previousSumCount);//读取笔数列数据
                }
            }

            //在原有数据的最后一行创建新的内容行
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow excelRow = sheet.CreateRow(previousLastRowNum + i);
                DataRow valueRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = excelRow.CreateCell(j);
                    cell.SetCellValue(valueRow[j].ToString());
                }
            }
            DataRow currLastRow = dt.Rows[dt.Rows.Count - 1];
            Decimal.TryParse(currLastRow["交易本金"].ToString(), out currSumTransMoney);
            Decimal.TryParse(currLastRow["手续费"].ToString(), out currSumTransFree);
            Int16.TryParse(currLastRow["分期手续费"].ToString(), out currSumCount);

            //创建最后一行合计行
            int lastRowNumber = previousLastRowNum + dt.Rows.Count + 1;
            IRow lastRow = sheet.CreateRow(lastRowNumber);
            ICell lastrow_cell0 = lastRow.CreateCell(0);
            ICell lastrow_cell7 = lastRow.CreateCell(7);
            ICell lastrow_cell8 = lastRow.CreateCell(8);
            ICell lastrow_cell9 = lastRow.CreateCell(9);

            lastrow_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            lastrow_cell7.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            lastrow_cell8.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            lastrow_cell9.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);


            lastrow_cell0.SetCellValue("大合计");
            lastrow_cell7.SetCellValue((previousSumTransMoney + currSumTransMoney).ToString());
            lastrow_cell8.SetCellValue((previousSumTransFree + currSumTransFree).ToString());
            lastrow_cell9.SetCellValue((previousSumCount + currSumCount).ToString());

            //写入文件
            try
            {
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                    isSuccessed = true;
                }
            }
            catch (Exception ex)
            {
                isSuccessed = false;
            }
          
            return isSuccessed;
        }
        #endregion

        #region 创建Excel的工作表《银联RD信用卡》 未用到
        /// <summary>
        /// 创建工作薄的工作表《通联支付信用卡》
        /// </summary>
        /// <param name="outputFilePath">输出路径</param>
        /// <param name="wk">工作薄</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="dt">导出的数据</param>
        /// <returns></returns>
        public static bool CreateExcelSheet2(string outputFilePath, HSSFWorkbook wk, string sheetName, DataTable dt)
        {
            bool isSuccessed = false;
            ISheet sheet = wk.CreateSheet(sheetName);

            //创建标题列
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;

            ICell row0_cell0 = row0.CreateCell(0);
            ICell row0_cell1 = row0.CreateCell(1);
            ICell row0_cell2 = row0.CreateCell(2);
            ICell row0_cell3 = row0.CreateCell(3);
            ICell row0_cell4 = row0.CreateCell(4);
            ICell row0_cell5 = row0.CreateCell(5);
            ICell row0_cell6 = row0.CreateCell(6);
            ICell row0_cell7 = row0.CreateCell(7);
            ICell row0_cell8 = row0.CreateCell(8);
            ICell row0_cell9= row0.CreateCell(9);

            row0_cell0.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell1.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell2.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell3.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell4.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell5.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell6.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell7.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell8.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);
            row0_cell9.CellStyle = StyleXlsHelper.GetCellStyle(wk, CellStyleType.头);

            row0_cell0.SetCellValue("结算日期");
            row0_cell1.SetCellValue("交易本金");
            row0_cell2.SetCellValue("手续费");
            row0_cell3.SetCellValue("笔数");
            row0_cell4.SetCellValue("发卡行");
            row0_cell5.SetCellValue("发卡行代码");
            row0_cell6.SetCellValue("卡种名称");
            row0_cell7.SetCellValue("银行卡类型");
            row0_cell8.SetCellValue("卡号长度");
            row0_cell9.SetCellValue("BIN号");
         
            //创建内容列
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow excelRow = sheet.CreateRow(i + 1);
                DataRow valueRow = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = excelRow.CreateCell(j);
                    cell.SetCellValue(valueRow[j].ToString());
                }
            }
            //写入文件
            try
            {
                using (FileStream stm = File.OpenWrite(outputFilePath))
                {
                    wk.Write(stm);
                    isSuccessed = true;
                }
            }
            catch (Exception ex)
            {
                isSuccessed = false;
            }

            return isSuccessed;
        }
        #endregion

    }
}
