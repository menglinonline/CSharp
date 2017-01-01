using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionList
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add(5);
            arrayList.Add(100);
            arrayList.Remove(5);
            arrayList.Add("jekexuyuan");
            //数组列表把每个元素都当成object，在列表里存储一个object引用，具体的值可能为任何类型
            //数组列表不是一个强类型的数据集合，在运行时容易出错
            foreach (var e in arrayList)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(arrayList[0]);

            //只能存储整数类型
            List<int> list = new List<int>();
            list.Add(500);
            list.AddRange(new int[] {501, 502});
            Console.WriteLine(list.Contains(200));
            Console.WriteLine(list.IndexOf(501));
            list.Remove(501);
            list.Insert(1, 505);
            Console.ReadLine();
        }
    }
}
