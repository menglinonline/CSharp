using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomizeAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            //反射自定义属性类
            Type type = typeof(MyClass1);
            var classAttribute = (HelperAttribute)Attribute.GetCustomAttribute(type, typeof(HelperAttribute));
            Console.WriteLine("{0}", classAttribute.PrintMessage());


            foreach (Attribute attr in typeof(SubMyClass1).GetCustomAttributes(true))
            {
                HelperAttribute helperAtt = attr as HelperAttribute;
                if (helperAtt != null)
                {
                    Console.WriteLine("Name:{0} Description:{1}",helperAtt.Name, helperAtt.Description);
                }
            }

            /**
             * 通过反射来获取Attribute中的信息
             **/
            foreach (Attribute customerAttr in typeof(MyClass2).GetCustomAttributes(true))
            {
                HelperAttribute helperAtt = customerAttr as HelperAttribute;
                if (helperAtt != null)
                {
                    Console.WriteLine("Name:{0} Description:{1}", helperAtt.Name, helperAtt.Description);
                }
            }


            /**
            * 通过反射来获取Attribute中的信息
            **/
            foreach (Attribute customerAttr in typeof(MyClass3).GetCustomAttributes(true))
            {
                HelperAttribute helperAtt = customerAttr as HelperAttribute;
                if (helperAtt != null)
                {
                    Console.WriteLine("Name:{0} Description:{1}", helperAtt.Name, helperAtt.Description);
                }
            }


            /*
             * 通过反射来获取Attribute中的信息
             */
            foreach (Attribute customerAttr in typeof(MyClass4).GetCustomAttributes(true))
            {
                HelperAttribute helperAtt = customerAttr as HelperAttribute;
                if (helperAtt != null)
                {
                    Console.WriteLine("Name:{0} Description:{1}", helperAtt.Name, helperAtt.Description);
                }
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 自定义属性类，需要继承Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class HelperAttribute : System.Attribute
    {
        private string _name;
        private string _description;

        public HelperAttribute(string des)
        {
            this._name = "Default name";
            this._description = des;
        }

        public HelperAttribute(string des1,string des2)
        {
            this._description = des1;
            this._description = des2;
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string PrintMessage()
        {
            return "PrintMessage";
        }
    }

    #region 测试属性类的继承
    [HelperAttribute("this is my class1")]
    public class MyClass1
    {

    }

    public class SubMyClass1 : MyClass1
    {

    }
    #endregion

    [HelperAttribute("this is my class2", Name = "myclass2")]
    public class MyClass2
    {

    }

    [HelperAttribute("this is my class3", Name = "myclass3", Description = "New Description")]
    public class MyClass3
    {

    }

    [HelperAttribute("this is my class4", "this is my classFour")]
    public class MyClass4
    {

    }
}
