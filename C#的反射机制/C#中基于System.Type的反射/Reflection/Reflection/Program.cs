using System;
using System.Collections.Generic;
using System.Text;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Type是抽象类，不能new Type()去实例化
            string str = "Hello Reflection";
            Type t = str.GetType();
            Console.WriteLine(t.FullName);
            //静态方法Type.GetType获取类型
            Type t2 = Type.GetType("System.String",false,true);
            Console.WriteLine(t2.FullName);
            //typeof运算符获取类型
            Type t3 = typeof(string);
            Console.WriteLine(t3.FullName);

            Console.ReadLine();
        }
    }
}
