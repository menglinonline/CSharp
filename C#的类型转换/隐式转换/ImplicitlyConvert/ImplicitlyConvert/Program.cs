using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplicitlyConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 30;
            long j = age;

            class1 c1 = new class2();
        }
    }

    class class1 
    {

    }

    class class2 : class1
    { 

    }
}
