using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            string m, n;
            a = 10;
            b = 20;
            m = "我";
            n = "叫";
            Console.WriteLine("a:{0};b:{1}",a,b);
            Console.WriteLine("m:{0};n:{1}", m, n);

            Swap<int>(ref a, ref b);
            Swap<string>(ref m, ref n);

            Console.WriteLine("a:{0};b:{1}", a, b);
            Console.WriteLine("m:{0};n:{1}", m, n);

            Console.ReadLine();
        }
       
        private static void Swap<T>(ref T j, ref T k)
        {
            T temp;
            temp = j;
            j = k;
            k = temp;
        }
    }

}
