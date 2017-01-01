using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountChecking
{
    class ExcelHelper
    {
        public void ReadFromExcelFile(string path)
        {
            IWorkbook wk = null;
            string extension = Path.GetExtension(path);
            try
            {
                FileStream fs = File.OpenRead(path);
                if (extension.Equals(".xls"))
                    //把xls文件中的数据写入wk中
                    wk = new HSSFWorkbook(fs);
                else
                    //把xlsx文件中的数据写入wk中
                    wk = new XSSFWorkbook(fs);

                fs.Close();
                //读取当前表数据
                ISheet sheet = wk.GetSheetAt(0);
                //读取当前行数据
                IRow row = sheet.GetRow(0);
                //LastRowNum 是当前表的总行数-1（注意）
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);//读取当前行数据
                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //读取该行的第j列数据
                            string value = row.GetCell(j).ToString();
                            Console.Write(value.ToString() + " ");
                        }
                        Console.WriteLine("\n");
                    }
                }
            }
            catch (Exception e)
            {
                //只在Debug模式下才输出
                Console.WriteLine(e.Message);
            }
            finally
            {
                wk = null;
            }
        }

        public static ISheet ReadSheetFromExcelFile(string filePath, int sheetIndex)
        {
            IWorkbook wk = null;
            string extension = System.IO.Path.GetExtension(filePath);
            try
            {
                FileStream fs = File.OpenRead(filePath);
                if (extension.Equals(".xls"))
                {
                    //把xls文件中的数据写入wk中
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    //把xlsx文件中的数据写入wk中
                    wk = new XSSFWorkbook(fs);
                }
                fs.Close();
                if (sheetIndex + 1 > wk.NumberOfSheets)
                {
                    throw new IndexOutOfRangeException("SheetIndex超出范围");
                }
                //读取当前表数据
                ISheet sheet = wk.GetSheetAt(sheetIndex);
                return sheet;
            }
            catch (Exception e)
            {
                //只在Debug模式下才输出
                Console.WriteLine(e.Message);
            }
            finally
            {
                wk = null;
            }
            return null;
        }

        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("sheet0");
            IRow headerRow = sheet.CreateRow(0);
            // handling header.       
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.        
            int rowIndex = 1;
            //新增的四句话，设置CELL格式为文本格式  
            ICellStyle cellStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            cellStyle.DataFormat = format.GetFormat("@");

            foreach (DataRow row in SourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    ICell cell = dataRow.CreateCell(column.Ordinal);
                    cell.CellStyle = cellStyle;
                    cell.SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;
            return ms;
        }

        public static void RenderDataTableToExcel(DataTable SourceTable, string FileName)
        {
            MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush(); fs.Close();
            data = null; ms = null; fs = null;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            ISheet sheet = workbook.GetSheet(SheetName);
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                    dataRow[j] = row.GetCell(j).ToString();
            }
            ExcelFileStream.Close(); workbook = null; sheet = null; return table;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            ISheet sheet = workbook.GetSheetAt(SheetIndex);
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }
                table.Rows.Add(dataRow);
            }
            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static void RenderAsEnumerableToExcel(IEnumerable<DataRow> Rows, DataColumnCollection Columns, string FileName)
        {
            MemoryStream ms = (MemoryStream)StreamAsEnumerableToExcel(Rows, Columns);
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;
        }

        public static Stream StreamAsEnumerableToExcel(IEnumerable<DataRow> Rows, DataColumnCollection Columns)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("sheet0");
            int rowIndex = sheet.LastRowNum;
            IRow headerRow = sheet.CreateRow(rowIndex);
            // handling header.       
            foreach (DataColumn column in Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.        
            foreach (DataRow row in Rows)
            {
                rowIndex++;
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in Columns)
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;
            return ms;
        }

        public static void WriteToExcel(IWorkbook workbook, string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            workbook.Write(fs);
            fs.Close();
        }
        
        public void WriteToExcel(string filePath)
        {
            //创建工作薄  
            IWorkbook wb;
            string extension = System.IO.Path.GetExtension(filePath);
            //根据指定的文件格式创建对应的类
            if (extension.Equals(".xls"))
            {
                wb = new HSSFWorkbook();
            }
            else
            {
                wb = new XSSFWorkbook();
            }

            ICellStyle style1 = wb.CreateCellStyle();//样式
            style1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
            style1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //设置边框
            style1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style1.WrapText = true;//自动换行

            ICellStyle style2 = wb.CreateCellStyle();//样式
            IFont font1 = wb.CreateFont();//字体
            font1.FontName = "楷体";
            font1.Color = HSSFColor.Red.Index;//字体颜色
            font1.Boldweight = (short)FontBoldWeight.Normal;//字体加粗样式
            style2.SetFont(font1);//样式里的字体设置具体的字体样式
            //设置背景色
            style2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            style2.FillPattern = FillPattern.SolidForeground;
            style2.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            style2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
            style2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式

            //创建一个表
            ISheet tb = wb.CreateSheet("sheet0");
            //设置列宽
            int[] columnWidth = { 10, 10, 10, 20 };

            //测试数据
            int rowCount = 3, columnCount = 4;
            object[,] data = {
                                {"列0", "列1", "列2", "列3"},
                                {"", 400, 5.2, 6.01},
                                {"", DateTime.Today, true, "2014-07-02"}
                            };

            for (int i = 0; i < columnWidth.Length; i++)
            {
                //设置列宽度，256*字符数，因为单位是1/256个字符
                tb.SetColumnWidth(i, 256 * columnWidth[i]);
            }

            IRow row;
            ICell cell;
            for (int i = 0; i < rowCount; i++)
            {
                row = tb.CreateRow(i);//创建第i行
                for (int j = 0; j < columnCount; j++)
                {
                    cell = row.CreateCell(j);//创建第j列
                    cell.CellStyle = j % 2 == 0 ? style1 : style2;
                    //根据数据类型设置不同类型的cell
                    cell.SetCellValue((string)data[i, j]);
                }
            }

            //合并单元格，如果要合并的单元格中都有数据，只会保留左上角的
            //CellRangeAddress(0, 2, 0, 0)，合并0-2行，0-0列的单元格
            CellRangeAddress region = new CellRangeAddress(0, 2, 0, 0);
            tb.AddMergedRegion(region);

            try
            {
                FileStream fs = File.OpenWrite(filePath);
                wb.Write(fs);   //向打开的这个xls文件中写入表并保存。  
                fs.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }
}
