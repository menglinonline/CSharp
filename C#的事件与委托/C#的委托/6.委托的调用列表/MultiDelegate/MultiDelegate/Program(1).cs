using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDelegate
{
    class Program
    {
        delegate void PersionChanger(int age);
        static void Main(string[] args)
        {
            PersionChanger pc1 = new PersionChanger(Persion.AddAge);
            pc1(30);
            Console.WriteLine();
            PersionChanger pc2 = new PersionChanger(Persion.AddHeigth);
            pc2(170);
            Console.WriteLine();
            PersionChanger pc3 = pc1 + pc2;
            pc3(10);
            Console.WriteLine();
            Persion persion = new Persion();
            PersionChanger pc4 = new PersionChanger(persion.AddWeight);
            pc3 += pc4;
            pc3(50);
            Console.WriteLine();
            pc3 += pc1;
            pc3(60);
            Console.WriteLine();
            pc3 -= pc1;
            pc3(70);
            Console.WriteLine();
            pc3 -= pc4;
            pc3(80);
            Console.WriteLine();
            pc3 -= pc2;
            pc3 -= pc1;
            pc3(90);

            Console.ReadLine();
        }
    }

    class Persion
    {
        public static void AddAge(int age)
        {
            Console.WriteLine("person age is {0}", age + 1);
        }

        public static void AddHeigth(int height)
        {
            Console.WriteLine("person height is {0}", height + 1);
        }

        public void AddWeight(int weight)
        {
            Console.WriteLine("person weight is {0}", weight + 1);
        }
    }
}
