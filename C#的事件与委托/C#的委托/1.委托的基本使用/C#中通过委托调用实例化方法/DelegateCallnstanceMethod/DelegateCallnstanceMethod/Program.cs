using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCallnstanceMethod
{
    class Program
    {
        delegate int AgeChanger(int age);
        static void Main(string[] args)
        {
            Persion persion = new Persion();
            AgeChanger ac = new AgeChanger(persion.GetAddAge);
            int result = ac(12);

            Console.WriteLine("Value of instance age {0}", persion.age);
            Console.WriteLine("Value of instance age {0}", result);

            AgeChanger ac2 = new AgeChanger(persion.GetMultiplyAge);
            int result2 = ac2(2);
            Console.WriteLine("Value of instance age {0}", persion.age);
            Console.WriteLine("Value of instance age {0}", result2);
            Console.ReadLine();
        }
    }

    class Persion
    {
        public int age = 20;

        public int GetAddAge(int myAge)
        {
            age += myAge;
            return age;
        }

        public int GetMultiplyAge(int myAge)
        {
            age *= myAge;
            return age;
        }
    }
}
