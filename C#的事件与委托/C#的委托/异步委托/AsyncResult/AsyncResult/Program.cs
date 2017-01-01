using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        delegate string MyMethodDelegate(); 
        static void Main(string[] args)
        {
            User user = new User();
            //方式一：同步调用方法，声明一个委托变量mydelegate，且绑定到动态方法GetNameFirst
            MyMethodDelegate my_delegate = new MyMethodDelegate(user.GetNameFirst);
            string strResult = my_delegate();
            Console.WriteLine("方法GetNameFirst()执行完成");
            Console.WriteLine(strResult);
         
            MyMethodDelegate my_delegate2 = new MyMethodDelegate(User.GetNameSecond);
            AsyncResult async_result;//此类封闭异步委托异步调用的结果，通过AsyncResult得到结果
            //开始调用
            async_result = (AsyncResult)my_delegate2.BeginInvoke(null, null);
            //判断线程是否执行完成
            while (!async_result.IsCompleted)
            {
                Console.WriteLine("正在异步执行方法GetNameSecond()......");
            }
            Console.WriteLine("方法GetNameSecond()执行完成");
            //等待委托调用的方法的完成
            string strResult2 = my_delegate2.EndInvoke(async_result);
            Console.WriteLine(strResult2);
            Console.ReadKey();
        }
    }

    class User
    {
        //要调用的动态方法
        public string GetNameFirst()
        {

            Thread.Sleep(5000);
            return "从小就犯困";
        }

        //要调用的静态方法
        public static string GetNameSecond()
        {

            return "从小就犯困";
        }
    }
}
