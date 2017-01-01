using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareTools.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 得到文件路径(年月日_文件名 命名)
        /// </summary>
        /// <param name="outputFolder">输出目录路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns>得到文件路径</returns>
        public static string GetCurentDayFilePath(string outputFolder, string fileName)
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            string txtFileName = fileName + "_" + today + ".txt";
            string txtValue = string.Empty;
            if (File.Exists(outputFolder + "\\" + txtFileName))
            {
                //读取文件
                using (FileStream fsRead = File.OpenRead(outputFolder + "\\" + txtFileName))
                {
                    //读成流
                    StreamReader sr = new StreamReader(fsRead, Encoding.Default);
                    while (!sr.EndOfStream)
                    {
                        txtValue = sr.ReadLine();
                    }
                    sr.Close();
                }
                using (FileStream fsWriter = File.OpenWrite(outputFolder + "\\" + txtFileName))
                {
                    StreamWriter sw = new StreamWriter(fsWriter, Encoding.Default);
                    sw.WriteLine((Convert.ToInt16(txtValue) + Convert.ToInt16(1)));
                    sw.Flush();
                    sw.Close();
                }

                return outputFolder + "\\" + fileName + "_" + today + "_" + (Convert.ToInt16(txtValue) + Convert.ToInt16(1)) + ".xls";
            }
            else
            {
                using (FileStream fsWriter = File.OpenWrite(outputFolder + "\\" + txtFileName))
                {
                    StreamWriter sw = new StreamWriter(fsWriter, Encoding.Default);
                    sw.WriteLine(1);
                    sw.Flush();
                    sw.Close();
                    return outputFolder + "\\" + fileName + "_" + today + "_" + 1 + ".xls";
                }
            }
        }

        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
    }
}
