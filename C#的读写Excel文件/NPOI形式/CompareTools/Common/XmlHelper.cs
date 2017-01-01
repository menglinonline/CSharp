using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CompareTools.Common
{
    public class XmlHelper
    {
        ///<summary>
        /// 选择匹配XPath表达式的所有子节点XmlNode.
        ///</summary>
        ///<param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        ///<param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        ///<returns>返回DataTable</returns>
        public static DataTable ReadXmlNodeByTrader(string xmlFileName, string xPath)
        {
            DataSet d = new DataSet(); //创建DataSet
            d.Tables.Add("trader");
            d.Tables["trader"].Columns.Add("Text");
            d.Tables["trader"].Columns.Add("Value");

            DataRow all = d.Tables["trader"].NewRow();
            all["Text"] = "所有商户";
            all["Value"] = "TraderSelect";
            d.Tables["trader"].Rows.Add(all);

            try
            {
                XmlDocument doc = new XmlDocument();

                // 获得配置文件的全路径　　
                doc.Load(xmlFileName);
                XmlNode node = doc.SelectSingleNode(xPath);
                XmlNodeList nodes = node.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].NodeType == XmlNodeType.Element)
                    {
                        DataRow row = d.Tables["trader"].NewRow();
                        row["Text"] = nodes[i].Attributes["name"].Value;
                        row["Value"] = nodes[i].InnerText;
                        d.Tables["trader"].Rows.Add(row);
                    }
                }
                return d.Tables["trader"];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据选择的菜单项，加载商户
        /// </summary>
        /// <param name="xmlFileNameList">文件名称列表</param>
        /// <param name="xPath">节点</param>
        /// <returns></returns>
        public static DataTable GetTraderListByXmlPath(List<string> xmlFileNameList, string xPath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");

            DataRow all = dt.NewRow();
            all["Text"] = "所有商户";
            all["Value"] = "TraderSelect";
            dt.Rows.Add(all);

            foreach (string xmlFileName in xmlFileNameList)
            {
                XmlDocument doc = new XmlDocument();
                //获得文件的路径　　
                doc.Load(System.Environment.CurrentDirectory + "/Trader/" + xmlFileName);
                XmlNode node = doc.SelectSingleNode(xPath);
                XmlNodeList nodes = node.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].NodeType == XmlNodeType.Element)
                    {
                        DataRow row = dt.NewRow();
                        row["Text"] = nodes[i].Attributes["name"].Value;
                        row["Value"] = nodes[i].InnerText;
                        dt.Rows.Add(row);
                    }
                }
            }
            //去掉重复行
            string[] columnNames = { "Text", "Value" };
            DataView dv = new DataView(dt);
            dt = dv.ToTable(true, columnNames);

            return dt;
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="xmlFileName">文件名称</param>
        /// <param name="xPath">节点</param>
        /// <param name="traderName">商户名</param>
        /// <param name="traderNo">商户号</param>
        public static void AddTrader(string xmlFileName, string xPath, string traderName, string traderNo)
        {
            string xmlPath = System.Environment.CurrentDirectory + "/Trader/" + xmlFileName;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode node = xmlDoc.SelectSingleNode(xPath);//查找<trader>
            XmlElement xe1 = xmlDoc.CreateElement("item");//创建一个<item>节点
            xe1.SetAttribute("name", traderName);//设置该节点genre属性
            xe1.InnerText = traderNo;//设置文本节点
            node.AppendChild(xe1);//添加到<bookstore>节点中
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 删除商户
        /// </summary>
        /// <param name="xmlFileName">文件名称</param>
        /// <param name="xPath">节点</param>
        /// <param name="traderName">商户名</param>
        /// <param name="traderNo">商户号</param>
        public static void DeleteXmlNodeByTrader(string xmlFileName, string xPath, string traderName, string traderNo)
        {
            string xmlPath = System.Environment.CurrentDirectory + "/Trader/" + xmlFileName;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode node = xmlDoc.SelectSingleNode(xPath);
            XmlNodeList nodes = node.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.InnerText.IndexOf(traderNo, 0) >= 0 && xe.Attributes["name"].Value.IndexOf(traderName, 0) >= 0)
                {
                    xn.ParentNode.RemoveChild(xn);
                }
            }
            xmlDoc.Save(xmlPath);
        }
    }
}
