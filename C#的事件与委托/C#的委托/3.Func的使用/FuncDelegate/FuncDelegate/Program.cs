using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Persion persion1 = new Persion();
            Persion persion2 = persion1.GetPersionInfo();

            Console.WriteLine("This persion name is {0}", persion2.Name);
            Console.WriteLine("This persion age is {0}", persion2.Age);

            Employee employee1 = new Employee();
            Employee employee2 = employee1.GetEmployeeInfo();

            Console.WriteLine("This employee post is {0}", employee2.Post);
            Console.WriteLine("This employee salary is {0}", employee2.Salary);

            Console.ReadLine();
        }
    }
}
