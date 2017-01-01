using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.for
            for (int k = 0; k < 5; k++)
            {
                Console.WriteLine(k);
                if (k == 3)
                {
                    continue;
                }
                Console.WriteLine("After Condition");
            }
            //更好的理解for循环3个位置的值
            int i;
            int j = 10;
            for (i = 0, Console.WriteLine("Start:{0}",i); i < j; i++,j--, Console.WriteLine("i={0}，j={1}",i,j))
            {
                Console.WriteLine("Body of the loop");
            }
            //初始值什么都不做，条件为什么都不做，最后什么都不做
            //for (; ; )
            //{
            //    Console.WriteLine("Can u stop");
            //}

            bool stop = false;
            for (; !stop; )
            {
                stop = true;
                Console.WriteLine("Can u stop");
            }
            //2.forearch 需要实现这个接口IEnumerable
            List<int> listInt = new List<int>() { 1, 2, 3, 4, 5 };
            foreach (var value in listInt)
            {
                Console.WriteLine(value);
            }
            //3.while
            int n = 1;
            //n++ n = n + 1 加之前去比较
            //++n           加完之后去比较
            while (n++ < 6)
            {
                Console.WriteLine("n is {0}",n);
            }
            int m = 1;
            while (++m < 6)
            {
                Console.WriteLine("m is {0}", m);
            }
            //4.do while
            int l = 1;
            do
            {
                Console.WriteLine("do");
            }
            while (l++ < 6);

            Console.ReadLine();
        }
    }
}
