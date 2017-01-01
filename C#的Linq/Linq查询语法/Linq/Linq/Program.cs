using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nuymbers = { 5, 10, 8, 3, 6, 12 };
            //1.Query syntax
            var numberQuery1 = from num in nuymbers
                               where num == 12
                               orderby num
                               select num;
            //2.Method syntax
            var numberQuery2 = nuymbers.Where(n => n % 2 == 0).OrderBy(n => n);

            foreach (var i in numberQuery1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in numberQuery2)
            {
                Console.WriteLine(i);
            }

            QuerySyntax();
            Console.ReadLine();
        }

        private static void QuerySyntax()
        {
            //1.Data Source
            int[] nuymbers = { 1, 2, 3, 4, 5, 6 };
            //2.Query creation
            var numberQuery = from num in nuymbers
                              where num % 2 == 0
                              orderby num
                              select num;
            numberQuery.ToList();
            numberQuery.ToArray();
            //3.Query execution
            foreach (var i in numberQuery)
            {
                Console.WriteLine(i);
            }
        }
    }
}
