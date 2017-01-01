using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 32;
            object oAge = age;

            int myAge = (int)oAge;

            object nullObject = 5;
            int iNull = (int)nullObject;

            int? iNullable = null;
            System.Nullable<int> iNullable2 = 100;

            Console.WriteLine(iNullable.HasValue);
            Console.WriteLine(iNullable.GetValueOrDefault());
            Console.WriteLine(iNullable2.GetValueOrDefault());

            int result = iNullable ?? 32;
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
