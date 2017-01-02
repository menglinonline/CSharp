using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDelegate
{
    delegate void D(int n);
    class Program
    {
        static void Main(string[] args)
        {
            //调用第一个静态方法
            D cd1 = new D(C.M1);
            cd1(-1);
            Console.WriteLine();

            //调用第二个静态方法
            D cd2 = new D(C.M2);
            cd2(-2);
            Console.WriteLine();

            //多重委托
            D cd3 = cd1 + cd2;
            cd3(10);
            Console.WriteLine();

            //多重委托
            C c = new C();
            D cd4 = new D(c.M3);
            cd3 += cd4;
            cd3(30);
            Console.WriteLine();

            cd3 += cd1;
            cd3(20);
            Console.WriteLine();

            //-=哪个cd1会被减去
            cd3 -= cd1;
            cd3(40);
            Console.WriteLine();

            cd3 -= cd2;
            cd3 -= cd4;
            cd3(50);
            Console.WriteLine(); 
           
            //当一个委托列表为空的时候，对他进行调用会报错
            cd3 -= cd1;
            cd3(60);

            Console.ReadLine();
        }
    }

    class C
    {
        public static void M1(int i)
        {
            Console.WriteLine("C.M1:" + i);
        }

        public static void M2(int i)
        {
            Console.WriteLine("C.M2:" + i);
        }

        public void M3(int i)
        {
            Console.WriteLine("C.M3:" + i);
        }
    }
}
