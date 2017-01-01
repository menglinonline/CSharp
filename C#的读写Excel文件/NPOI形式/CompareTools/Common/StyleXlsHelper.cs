using CompareTools.Common.Enumeration;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareTools.Common
{
    public class StyleXlsHelper
    {
        /// <summary>
        /// 得到列样式
        /// </summary>
        /// <param name="wb">工作薄</param>
        /// <param name="styleXls">列风格</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(HSSFWorkbook wb, CellStyleType styleXls)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();

            //定义几种字体  
            //也可以一种字体，写一些公共属性，然后在下面需要时加特殊的  
            IFont font12 = wb.CreateFont();
            font12.FontHeightInPoints = 12;
            font12.FontName = "微软雅黑";
            font12.Boldweight = 70;
            //上面基本都是设共公的设置  
            //下面列出了常用的字段类型  
            switch (styleXls)
            {
                case CellStyleType.头:
                    // cellStyle.FillPattern = FillPatternType.LEAST_DOTS;  
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.时间:
                    IDataFormat datastyle = wb.CreateDataFormat();

                    cellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.数字:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.钱:
                    IDataFormat format = wb.CreateDataFormat();
                    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.url:
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.百分比:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.中文大写:
                    IDataFormat format1 = wb.CreateDataFormat();
                    cellStyle.DataFormat = format1.GetFormat("[DbNum2][$-804]0");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.科学计数法:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    cellStyle.SetFont(font12);
                    break;
                case CellStyleType.默认:
                    cellStyle.SetFont(font12);
                    break;
            }
            return cellStyle;


        }
     
    }
}
