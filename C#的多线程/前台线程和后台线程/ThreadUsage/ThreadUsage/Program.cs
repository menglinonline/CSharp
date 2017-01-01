using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadUsage
{
    class Program
    {
        public static void ThreadProcess()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProcess: {0}", i);
                //阻塞当前的线程thread1，执行别的进程也就是Main这个进程，线程被阻塞的毫秒数为0则表示应挂起此线程（也就是thread1这个线程）以便其他等待线程能够执行（也就是Main这个进程）
                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("在主进程Main中启动一个线程");
            //创建一个线程
            Thread thread1 = new Thread(new ThreadStart(ThreadProcess));
            //启动此线程
            thread1.Start();
            //创建一个线程
            Thread thread2 = new Thread(new ThreadStart(ThreadProcess));
            //启动此线程      　　
            thread2.Start();
            //挂起此线程　
            thread2.Suspend();　
　　        for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("主进程Main输出.....");
　　　　　　　　//阻塞当前的主进程Main，执行别的进程也就是thread1这个线程，线程被阻塞的毫秒数为0则表示应挂起此线程（也就是Main这个进程）以便其他等待线程能够执行（也就是thread1这个线程）
　　　　　　　　Thread.Sleep(0);          　
　　　　　　 }
           　Console.WriteLine("主线程Main调用线程Join方法直到thread1线程结束");
　　　　　　 //阻塞调用线程，直到某个线程终止时为止，也就是说阻塞主进程Main，直到thread1线程执行完毕
   　　　　　thread1.Join();
　　      　 Console.WriteLine("thread1线程结束");
             thread2.Resume();//恢复挂起的线程
　　　　　　 //thread2.IsBackground = true;
             Console.ReadKey();
        }
    }
}
