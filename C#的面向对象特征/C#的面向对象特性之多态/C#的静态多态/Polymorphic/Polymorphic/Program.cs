using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphic
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHello();
            PrintHello("world");

            Complex c1 = new Complex();
            Complex c2 = new Complex();
            c1.Number = 2;
            c2.Number = 3;

            Console.WriteLine((c1-c2).Number);

            Console.ReadLine();
        }

        public static void PrintHello()
        {
            Console.WriteLine("Hello");
        }

        public static void PrintHello(string welcome)
        {
            Console.WriteLine("Hello {0}", welcome);
        }

        //public static string PrintHello()
        //{
        //    return "Hello";
        //}
    }

    class Complex
    {
        public int Number
        {
            get;
            set;
        }

        /// <summary>
        /// 运算符重载
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Complex operator -(Complex c1,Complex c2)
        {
            Complex c = new Complex();
            c.Number = c1.Number + c2.Number;

            return c;
        }
    }
}
