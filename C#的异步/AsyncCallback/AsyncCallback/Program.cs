using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        public class UseDelegateForAsyncCallback
        {
            public static void Main()
            {
                AsyncCallback callBack = new AsyncCallback(PrintName);

                Dns.BeginGetHostEntry("31111", callBack, "32");

                Console.WriteLine("不必阻塞性地等待返回值或消息");
                Console.ReadLine();
            }

            static void PrintName(IAsyncResult result)
            {
                string hostName = (string)result.AsyncState;
                Console.WriteLine(hostName);
            }
        }
    }
}
