using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    public class TransformHelper
    {
        #region Dictionary和Json的互相转化
        /// <summary>
        /// Dictionary转化为Josn
        /// </summary>
        /// <param name="dic">dictionary</param>
        /// <returns></returns>
        public static string DictionaryToJson(Dictionary<string, string> dic)
        {
            //foreach (KeyValuePair<string, string> kv in dic)
            //{
            //    if (kv.Value == "")
            //    {
            //        dic.Remove(kv.Key);
            //    }
            //}

            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string json = (new JavaScriptSerializer()).Serialize(dic);

                return json;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Josn转化为Dictionary
        /// </summary>
        /// <param name="jsonData">josn格式字符串</param>
        /// <returns></returns>
        public static Dictionary<string, string> JsonToDictionary(string jsonData)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的JSON字符串转换为Dictionary<string, string>类型的对象
                return jss.Deserialize<Dictionary<string, string>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 对象和Json的互相转化
        /// <summary>
        /// josn转化为对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonData">josn格式字符串</param>
        /// <returns></returns>
        public static T JsonToObj<T>(string jsonData)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的JSON字符串转换为泛型的对象
                return jss.Deserialize<T>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 对象转化为josn
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">josn对象</param>
        /// <returns></returns>
        public static string ObjToJson(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的泛型的对象转换为JSON字符串
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 对象和Dictionary的互相转化
        /// <summary>
        /// Dictionary转化为对象
        /// </summary>
        /// <param name="dic">dictionary</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static T DictionaryToObj<T>(Dictionary<string, string> dic)
        {
            return default(T);
        }

        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }

            return map;

        }


        #endregion

        #region 对象和Xml的互相转化
        /// <summary>
        /// 对象转化为Xml
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string ObjToXml<T>(T t)
        {
            string xmlString = "";
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (System.IO.MemoryStream mem = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(mem, settings))
                {
                    writer.WriteStartDocument(true);
                    //去除默认命名空间xmlns:xsd和xmlns:xsi
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    formatter.Serialize(writer, t, ns);
                }
                xmlString = Encoding.UTF8.GetString(mem.ToArray());
            }

            return xmlString;
        }

        /// <summary>
        /// Xml转化为对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns></returns>
        public static T XmlToObj<T>(string xml)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T obj = (T)xs.Deserialize(stream);

            return obj;
        }
        #endregion
    }
}
