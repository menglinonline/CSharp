using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            //key value对，通过key访问，Hashtable同样不是强类型容易在运行时出错
            //使用无效的key，会返回一个空，不会报错
            Hashtable ht = new Hashtable();
            ht.Add("first","jike");
            ht.Add("secornd", "xuyuan");
            ht.Add(100, 1100);
            Console.WriteLine(ht["secornd"]);
            Console.WriteLine(ht[100]);
            Console.WriteLine(ht[99]);

            //替代Hashtable, 使用无效的key，会报错
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("first", "jike");
            Console.WriteLine(dict["99"]);

            //根据key值排序
            SortedList<int,int> sl = new SortedList<int,int>();
            sl.Add(5,105);
            sl.Add(2,102);
            sl.Add(10,99);
            foreach(var sle in sl)
            {
                Console.WriteLine(sle);
                Console.WriteLine(sle.Value);
            }
            //stack 先进后出，queue 先进先出
            Console.ReadLine();
        }
    }
}
