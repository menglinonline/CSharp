using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializeredToString
{
    class Program
    {
        static void Main(string[] args)
        {
            Persion per = new Persion();
            per.Name = "张三";
            per.Age = 35;

            string str = GetSerializeredString<Persion>(per);
            string str2 = GetSerializeredString(per);
        }


        public static string GetSerializeredString(Object Obj)
        {
            string xmlString = "";
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            //settings.OmitXmlDeclaration = true;
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
                    XmlSerializer formatter = new XmlSerializer(Obj.GetType());
                    formatter.Serialize(writer, Obj, ns);
                }
                xmlString = Encoding.UTF8.GetString(mem.ToArray());
            }

            return xmlString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string GetSerializeredString<T>(T t)
        {
            string xmlString = "";
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            //settings.OmitXmlDeclaration = true;
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
    }

    public class Persion
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _age;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }
}
