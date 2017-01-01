using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal sum1 = 123456.784M;
            decimal sumA = decimal.Round(sum1, 2, MidpointRounding.AwayFromZero);

            decimal sum2 = 123456.785M;
            decimal sumB = decimal.Round(sum2, 2, MidpointRounding.AwayFromZero);
        }
    }
}
