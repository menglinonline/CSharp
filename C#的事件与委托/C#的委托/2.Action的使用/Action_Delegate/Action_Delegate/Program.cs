using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action_Delegate
{
    public class Program
    {
        static void Main(string[] args)
        {
            Method1();
        }

        public static void Method1()
        {
            TryExecute(() =>
                {
                   //logic
                   Console.WriteLine("I am Method1");
                   Console.ReadLine();
                }, "Method1");

            //TryExecute(() => { }, "Method2");
        }


        public static void TryExecute(Action action, string funName = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
