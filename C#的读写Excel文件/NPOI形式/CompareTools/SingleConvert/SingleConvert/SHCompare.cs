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
using System.Xml;
using System.Configuration;

namespace AccountChecking
{
    class SHCompare
    {
        private string[] fileYH;
        private string strPath;//文件保存路径
        private Encoding enType;//RD文件编码格式默认
        private List<string> merchant;//所有商户列表
        private string tradeStartDate = String.Empty;//对账开始时间
        private string tradeEndDate = String.Empty;//对账结束时间
        private string traderCode = "TraderSelect";  //选择商户号：默认选择所有商户 

        /// <summary>
        /// Initializes a new instance of the <see cref="SHCompare"/> class.
        /// </summary>
        /// <param name="fileYH">The file yh.</param>
        /// <param name="traderCode">The trader code.</param>
        /// <param name="encodeType">Type of the encode.</param>
        public SHCompare(IEnumerable<string> fileYH, string traderCode, string strPath, Encoding encode)
        {
            this.fileYH = fileYH.ToArray();
            this.traderCode = traderCode;
            this.strPath = strPath;
            this.enType = encode ?? Encoding.Default;
            //SetDateRange(fileSrc);
            this.merchant = ReadXmlTenant();
        }

        private static List<string> ReadXmlTenant()
        {
            List<string> merchant = new List<string>();
            string xmlPath = "trader";
            //获得配置文件的全路径　　
            string xmlName = System.Environment.CurrentDirectory + "/Trader/Trader2.xml";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlName);
                XmlNode node = doc.SelectSingleNode(xmlPath);
                XmlNodeList nodes = node.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].NodeType == XmlNodeType.Element)
                        merchant.Add(nodes[i].InnerText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序发生错误：" + ex.Message);
            }
            return merchant;
        }

        //private void SetDateRange(int fileSrc)
        //{
        //    DateTime dt;
        //    if (fileSrc == 1)
        //    {
        //        IEnumerable<string> order = this.fileYH.OrderBy(m => m.ToLower());
        //        //int cnt = this.fileYH.Length;
        //        string date = String.Empty;
        //        //开始时间 //RD200715100300 => 15100300
        //        string fileName = order.FirstOrDefault();
        //        date = fileName.Substring(fileName.LastIndexOf("\\") + 7);
        //        //DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        //        //dtFormat.ShortDatePattern = "yyyyMMddHHmmss";
        //        //dt = Convert.ToDateTime(date, dtFormat);
        //        dt = DateTime.ParseExact(date, "yyMMddHH", CultureInfo.CurrentCulture);
        //        this.tradeStartDate = dt.AddHours(-1).ToString("yyyyMMddHHmmss");

        //        //结束时间 //RD200715100300 => 15100300
        //        string fileName2 = order.LastOrDefault();
        //        date = fileName2.Substring(fileName.LastIndexOf("\\") + 7);
        //        dt = DateTime.ParseExact(date, "yyMMddHH", CultureInfo.CurrentCulture);
        //        this.tradeEndDate = dt.AddHours(23).ToString("yyyyMMddHHmmss");
        //    }
        //    else if (fileSrc == 2)
        //    {
        //        IEnumerable<string> order = this.fileYH.OrderBy(m => m.ToLower());
        //        //int cnt = this.fileYH.Length;
        //        string date = String.Empty;
        //        //开始时间 //INN15101288ZM_826520148160010 => 151012
        //        string fileName = order.FirstOrDefault(); ;
        //        date = fileName.Substring(fileName.LastIndexOf("\\") + 4, 6);
        //        dt = DateTime.ParseExact(date, "yyMMdd", CultureInfo.CurrentCulture);
        //        this.tradeStartDate = dt.AddHours(-1).ToString("yyyyMMddHHmmss");

        //        //结束时间 //INN15101288ZM_826520148160010 => 151012
        //        string fileName2 = order.LastOrDefault();
        //        date = fileName2.Substring(fileName.LastIndexOf("\\") + 4, 6);
        //        dt = DateTime.ParseExact(date, "yyMMdd", CultureInfo.CurrentCulture);
        //        this.tradeEndDate = dt.AddHours(23).ToString("yyyyMMddHHmmss");
        //    }
        //}

        public void Compare()
        {
            if (fileYH == null) return;

            //银联原始文件读取
            string year = DateTime.Now.Year.ToString();
            string strTenantCode = "";//商户编号
            Regex regStart = new Regex(@"==============================");
            Regex regTenant = new Regex(@"商户编号：\s*(\S{1,})");
            Regex regSubtotal = new Regex(@"小计\s*(\d{1,})\s*(\S{1,})\s*(\S{1,})\s*(\S{1,})");

            bool tarTrader = "TraderSelect".Equals(traderCode) ? true : false;//处理?     
            string filter = ConfigurationManager.AppSettings["DataFilterSet"].ToString();
            bool isEnable = "ENABLE".Equals(filter.ToUpper());

            for (int i = 0; i < fileYH.Length; i++)
            {
                string fileName = fileYH[i];
                StringBuilder sb = new StringBuilder();
                //已经处理商户编号列表
                Dictionary<string, Dictionary<int, decimal>> tenantSet = new Dictionary<string, Dictionary<int, decimal>>();

                FileStream fileStream = System.IO.File.OpenRead(fileName);
                StreamReader streamReader = new System.IO.StreamReader(fileStream, enType);
                try
                {
                    string strStart = "";
                    string strTitle = "";
                    int subLoop = 0;
                    int isFilter = -1;//过滤中间数据，仅保留标题小计
                    bool isHeader = true;//是否添加商户标题信息
                    StringBuilder sbPart = new StringBuilder();

                    while (!streamReader.EndOfStream)
                    {
                        string strLine = streamReader.ReadLine();

                        //商户记录开始位置
                        System.Text.RegularExpressions.Match regStartMatch = regStart.Match(strLine);

                        if (regStartMatch.Success)
                        {
                            subLoop = 0;//小计处理
                            sbPart = new StringBuilder();
                            isFilter = 0;//保留数据标记
                            isHeader = true;//默认添加标题信息
                            tarTrader = false;//默认非处理对象
                            strStart = strLine;
                            //中国银联直联商户清算交易明细表    
                            strTitle = streamReader.ReadLine();
                            strLine = streamReader.ReadLine();
                            //商户编号 标题行
                            strLine = streamReader.ReadLine();
                        }

                        //商户编号验证
                        System.Text.RegularExpressions.Match regTenantMatch = regTenant.Match(strLine);
                        if (regTenantMatch.Success)
                        {
                            //特殊处理用 文件格式不正确时
                            subLoop = 0;//小计处理
                            sbPart = new StringBuilder();
                            isFilter = 0;//保留数据标记
                            isHeader = true;//默认添加标题信息
                            tarTrader = false;//默认非处理对象

                            strTenantCode = regTenantMatch.Groups[1].Value;
                            //所有商户信息转化
                            if ("TraderSelect".Equals(traderCode))
                                tarTrader = merchant.Exists(m => { return strTenantCode.Equals(m); });
                            else if (strTenantCode.Equals(traderCode))
                                tarTrader = true;
                        }

                        if (tarTrader)
                        {
                            if (isHeader)
                            {
                                isHeader = false;
                                sbPart.AppendLine(strStart);
                                sbPart.AppendLine(strTitle);
                                sbPart.AppendLine();
                            }

                            if (isEnable)
                            {
                                //首次读取终端编号时打开过滤
                                if (strLine.StartsWith(@"终端编号") && isFilter == 0)
                                    isFilter = 1;//过滤数据标记
                                else if (strLine.StartsWith(@"终端编号") && isFilter == 1)
                                    isFilter = 0;//保留数据标记

                                if (isFilter == 0)
                                    sbPart.AppendLine(strLine);
                            }
                            else
                                sbPart.AppendLine(strLine);                            

                            System.Text.RegularExpressions.Match subtotalMatch = regSubtotal.Match(strLine);
                            if (subtotalMatch.Success && ++subLoop > 1)
                            {
                                subLoop = 0;
                                string subtotalstring = subtotalMatch.Value;
                                int tradeCnt = Convert.ToInt32(subtotalMatch.Groups[1].Value);
                                decimal tradeSum = Convert.ToDecimal(subtotalMatch.Groups[2].Value);
                                //未处理的商户编号
                                if (!tenantSet.Keys.Contains(strTenantCode))
                                {
                                    Dictionary<int, decimal> subTotal = new Dictionary<int, decimal>();
                                    subTotal.Add(tradeCnt, tradeSum);
                                    tenantSet.Add(strTenantCode, subTotal);
                                    sb.AppendLine(sbPart.ToString());
                                }
                                else
                                {
                                    Dictionary<int, decimal> subTotal = tenantSet[strTenantCode];
                                    int oldCnt = subTotal.Keys.FirstOrDefault();
                                    decimal oldSum = subTotal.Values.FirstOrDefault();
                                    if (oldCnt != tradeCnt || oldSum != tradeSum)
                                        sb.AppendLine(sbPart.ToString());
                                }
                            }

                        }
                        else
                            continue;
                    }

                    if (sb.Length > 0)
                        StreamFileSave(fileName, sb);
                }
                catch (Exception e)
                {
                    fileStream.Close();
                    streamReader.Close();
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void StreamFileSave(string fileName, StringBuilder sb)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                //新文件保存路径
                //string strPath = String.Format("{0}/Convert", System.Environment.CurrentDirectory);
                //string strName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                //string strFile = String.Format("{0}/Convert/{1}", System.Environment.CurrentDirectory, strName);

                //if (!Directory.Exists(strPath))
                //    Directory.CreateDirectory(strPath);

                string strName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                string strFile = String.Format("{0}/{1}", strPath, strName);
                fs = new FileStream(strFile, FileMode.Create);
                sw = new StreamWriter(fs, enType);
                sw.Write(sb.ToString());//写入文件
                sw.Flush(); //清空缓冲区
                sw.Close(); //关闭流
                fs.Close();
            }
            catch (Exception e)
            {
                //关闭流,释放资源
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }
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
    }
}
