using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SimpleAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass.Message("In main function 38");
            //Function1();

            Console.ReadLine();
        }
        
        [Obsolete("Don't use old method",true)]
        static void Function1()
        {
            MyClass.Message("In main function123456");
        }
    }

    public class MyClass
    {
        [Conditional("DEBUG")]
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
