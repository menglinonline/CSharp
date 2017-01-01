using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountChecking
{
    class CSVFileHelper
    {
        private static string[] _headerName = null ;

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //Encoding.Default、UTF8
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            //string fileContent = sr.ReadToEnd();
            try
            {
                //标示列数
                int colCount = 0;
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine = null;

                //读取首行各列的标题
                string strHeader = sr.ReadLine();
                if (String.IsNullOrEmpty(strHeader))
                {
                    MessageBox.Show("CSV文件内容为空，请检查该文件！");
                    return null;
                }

                _headerName = strHeader.Split(',') ?? null;
                colCount = _headerName.Length;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < colCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
                if (aryLine != null && aryLine.Length > 0)
                {
                    //按照首行交易流水号字段升序排序
                    dt.DefaultView.Sort = _headerName[0] + " " + "asc";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("CSV文件读取失败！ " + e.Message);
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return dt;
        }

        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        public static void SaveCSV(DataTable dt, string fullPath, Encoding encoding)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists) fi.Directory.Create();
            FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs,encoding??Encoding.UTF8);
            try
            {
                StringBuilder strBuild = new StringBuilder("");
                //各列标题名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strBuild.Append(dt.Columns[i].ColumnName.ToString());
                    strBuild.Append((i < dt.Columns.Count - 1) ? "," : "");
                }
                sw.WriteLine(strBuild.ToString());

                //各行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBuild = new StringBuilder("");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();

                        //含逗号/回车/换行符的需要放到引号中
                        str = str.Replace(",", "\",\"")
                                 .Replace("\r", "\"\r\"")
                                 .Replace("\n", "\"\n\"");
                        //英文双引号转义
                        str = str.Replace("\"", "\"\"");
                        strBuild.Append(str);
                        strBuild.Append((j < dt.Columns.Count - 1) ? "," : "");
                    }
                    sw.WriteLine(strBuild.ToString());
                }

                MessageBox.Show("CSV文件保存成功！");
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSV文件保存失败！" + ex.Message);
            }
            finally {
                sw.Close();
                fs.Close();
            }            
        }       

        /// <summary>
        /// 读取大数据量CSV文件内容并存储数据库
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>void</returns>
        public static void SaveDataByCSV(string filePath)
        {         
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            SqlConnection sqlCon = new SqlConnection(@"Server=USER-20141013ZF\SQL2008;Database=db_SDPay;Uid=sa;Pwd=xA123456");
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            //开始事务处理
            SqlTransaction sqltran = sqlCon.BeginTransaction();

            SqlCommand mysqlcmd = new SqlCommand();
            mysqlcmd.Connection = sqlCon;
            mysqlcmd.Transaction = sqltran;
            string sql = "INSERT INTO IWEB_MOBILE_PC(BUSINESSMANID,BUSINESSMANNAME,INTOBANK1,INTOBANK2,INTOAMOUNT,APPLICATIONTIME,PROCESSINGTIME,BANKCODE) VALUES({0})";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string strCheck = "";
            try
            {
                //标示列数
                int colCount = 0;
                //记录每次读取的一行记录
                string strLine = "";

                //读取首行各列的标题
                string strHeader = sr.ReadLine();
                if (String.IsNullOrEmpty(strHeader))
                {
                    MessageBox.Show("CSV文件内容为空，请检查该文件！");
                    return null;
                }

                _headerName = strHeader.Split(',') ?? null;
                colCount = _headerName.Length;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    string busId = String.IsNullOrEmpty(aryLine[15]) ? "NULL" : aryLine[15];
                    string busName = String.IsNullOrEmpty(aryLine[30]) ? "NULL" : aryLine[30];

                    string str = busId + "," + "'" + busName + "','"
                                + aryLine[4] + "','" + aryLine[5] + "',"
                                + aryLine[6] + ",'" + aryLine[10] + "','"
                                + aryLine[11] + "','" + aryLine[31] + "'";
                    strCheck = String.Format(sql, str);
                    mysqlcmd.CommandText = strCheck;
                    mysqlcmd.ExecuteNonQuery();

                }
                sqltran.Commit();
                MessageBox.Show("CSV文件已经存储于数据库中【OK！】 ");
            }
            catch (Exception e)
            {
                MessageBox.Show("CSV文件操作过程中发生异常！ " + strCheck + e.Message);
            }
            finally
            {
                sr.Close();
                fs.Close();
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                }
            }
        }
    }
}
