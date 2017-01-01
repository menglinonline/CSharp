using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionMethodAndProperty
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Hello Reflection";
            Type t = str.GetType();
            Console.WriteLine(t.FullName);

            //得到全部方法
            //GetMethods(t);
            //得到特定的方法，缩小范围
            GetMethods(t, BindingFlags.Public | BindingFlags.Instance);
            //得到Copy专门方法
            Console.WriteLine("Join Method:{0}", t.GetMethod("Copy"));
            //GetFields()获取字段
            //GetProperties()//获取属性

            Console.ReadLine();
        }

        /// <summary>
        /// 得到类型的方法
        /// </summary>
        /// <param name="t">类型</param>
        public static void GetMethods(Type t)
        {
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                Console.WriteLine("{0}", m.Name);
            }
        }

        /// <summary>
        /// 得到类型的方法
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="f">枚举标签</param>
        public static void GetMethods(Type t, BindingFlags f)
        {
            MethodInfo[] mi = t.GetMethods(f);
            foreach (MethodInfo m in mi)
            {
                Console.WriteLine("{0}", m.Name);
            }
        }
    }
}
