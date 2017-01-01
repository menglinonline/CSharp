using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleepUsage
{
    class Program
    {
        delegate void OutPutDelegate();
        static void Main(string[] args)
        {
            Console.WriteLine("Now main thread begin");
            OutPutDelegate outPutDelegate = new OutPutDelegate(OutPutData);
            Thread thread = new Thread(new ThreadStart(outPutDelegate));
            thread.Start();
            for (int i = 0; i < 5; i++)
            {
                 Console.WriteLine(i);
                 Thread.Sleep(0);
            }
            Console.ReadKey();
        }

         private static void OutPutData()
         {
            Console.WriteLine("Orther Thread");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
         }
    }
}
