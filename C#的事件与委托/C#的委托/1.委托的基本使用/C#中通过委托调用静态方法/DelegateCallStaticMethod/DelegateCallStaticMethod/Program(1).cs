using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCallStaticMethod
{
    delegate int NumberChanger(int n);
    class Program
    {
        static int num = 10;
        static void Main(string[] args)
        {
            //实例化委托
            NumberChanger nc = new NumberChanger(AddNum);
            nc(25);
            Console.WriteLine("value of {0}", num);
            Console.ReadLine();
        }

        //静态方法
        public static int AddNum(int p)
        {
            num += p;

            return num;
        }
    }
}
