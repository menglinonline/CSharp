using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            double price = 12.5;
            int intPrice = (int)price;
            Console.WriteLine(intPrice);
            class1 class1 = new class1();
            try
            {
                class2 class2 = (class2)class1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(class1 is class1);
            Console.WriteLine(class1 is class2);

            class2 class22 = class1 as class2;
            Console.WriteLine(class22 == null);
           
            Console.ReadLine();
        }
    }

    class class1
    { 
    
    }

    class class2:class1
    { 
    
    }
}
