using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 15;
            int j = 6;
            int yushu=100;
            Person person = new Person();
            int value = person.GetShangAndYu(i, j, out yushu);
            Console.WriteLine("{0}/{1}={2}—{3}", i, j, value, yushu);
            Console.ReadKey();
        }
    }

    class Person
    {
        public int GetShangAndYu(int i, int j, out int yushu)
        {
            yushu = i % j;
            return i / j;
        }
    }
}
