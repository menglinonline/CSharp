using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.if else
            object nullobject = null;
            if (!false | (bool)nullobject)
            {
                Console.WriteLine("True Condition");
            }

            //2.?:
            int ten = 101;
            int result = ten < 10 ? ten : 99;
            Console.WriteLine(result);

            //3.switch
            int switchKey = 100;
            switch (switchKey)
            {
                case 10:
                    Console.WriteLine("switchKey is 10");
                    break;
                case 100:
                    Console.WriteLine("switchKey is 100");
                    break;
                default:
                    Console.WriteLine("I don't konw switchKey");
                    break;
            }
            Console.ReadLine();
        }
    }
}
