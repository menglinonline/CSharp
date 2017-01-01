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

namespace MerchantsinfoBankCardTrans
{
    public class Utility
    {
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
        public static bool CreateExcelFile(string outputFolderPath, string fileName, string settleDate, string transMoney,string free)
        {
            bool isSuccessed = false;
            //创建文件夹
            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
            //文件路径
            string excelPath = outputFolderPath + "\\" + fileName;
            //删除已经存在的文件
            if (File.Exists(excelPath))
            {
                File.Delete(excelPath);
            }

            if (!File.Exists(excelPath))
            {
                try
                {
                    //创建文件
                    FileStream fs = new FileStream(excelPath, FileMode.OpenOrCreate);
                    fs.SetLength(0);
                    fs.Write(Properties.Resources.Excel635353311441737186, 0, Properties.Resources.Excel635353311441737186.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (System.Exception ex)
                {
                    excelPath = string.Empty;

                    return isSuccessed;
                }
                //给Excel文件添加"Everyone,Users"用户组的完全控制权限
                FileInfo fi = new FileInfo(excelPath);
                System.Security.AccessControl.FileSecurity fileSecurity = fi.GetAccessControl();
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                fi.SetAccessControl(fileSecurity);

                //给Excel文件所在目录添加"Everyone,Users"用户组的完全控制权限
                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(excelPath));
                System.Security.AccessControl.DirectorySecurity dirSecurity = di.GetAccessControl();
                dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                di.SetAccessControl(dirSecurity);

                //定义OleDB连接字符串
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "data source=" + @excelPath + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = strConn;
                try
                {
                    //打开创建的Excel文件并写入值
                    conn.Open();
                    OleDbCommand cmd = null;
                    //向"Sheet1"表中插入几条数据,访问Excel的表的时候需要在表名后添加"$"符号,Insert语句可以不指定列名
                    cmd = new OleDbCommand("Insert Into [Sheet1$] Values('" + settleDate + "', '" + transMoney + "', '" + free + "')", conn);//(A,B,C) 
                    cmd.ExecuteNonQuery();
                    isSuccessed = true;
                }
                catch (Exception ex)
                {
                    return isSuccessed;
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

            return isSuccessed;
        }
    }
}
