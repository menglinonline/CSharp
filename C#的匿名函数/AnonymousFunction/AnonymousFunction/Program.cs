using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnonymousFunction
{
    class Program
    {
        delegate void TestDelegate(string str);

        delegate int TestDelegate2(int i);

        delegate TResult TestDelegate3<TArg, TResult>(TArg arg);

        static void Main(string[] args)
        {
            ////最原始的委托的写法
            //TestDelegate testDelegate = new TestDelegate(PrintStr);

            ////C# version 2.0,Anonymous Method
            //TestDelegate testDelegate2 = delegate(string str) { Console.WriteLine(str); };

            ////C# version 3.0,Lambda Expression
            //TestDelegate testDelegate3 = (x) => { Console.WriteLine(x); };

            //testDelegate("Hello this is delegate");
            //testDelegate2("Hello this is anonymous method");
            //testDelegate3("Hello this is lambda expression");

            //StartThread();

            Lambda();
            Console.ReadLine();
        }

        private static void PrintStr(string str)
        {
            Console.WriteLine(str);
        }

        private static void StartThread()
        {
            Thread thread = new Thread(
                delegate()
                {
                    Console.Write("Hello ");
                    Console.WriteLine("World");
                }
            );
            thread.Start();
        }

        private static void Lambda()
        {
            //() => expression
            TestDelegate2 del2 = (x) => x * x;
            Console.WriteLine(del2(5));

            TestDelegate3<int, bool> del3 = x => x == 5;
            Console.WriteLine(del3(5));
        }
    }
}
