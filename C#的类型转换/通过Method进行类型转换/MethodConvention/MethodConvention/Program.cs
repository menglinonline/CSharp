using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodConvention
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 30;
            string a = age.ToString();

            int myAge = Convert.ToInt32("32");
            int myAge2 = Int32.Parse("32");
            int myAge3;
            bool succeed = Int32.TryParse("32", out myAge3);

            Console.WriteLine(myAge3);
            Console.ReadLine();
        }
    }
}
