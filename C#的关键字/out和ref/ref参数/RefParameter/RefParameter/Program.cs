using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            string sex = "男";
            Person person = new Person();
            string name = person.GetName(ref sex);
            Console.WriteLine("{0}{1}", name, sex);
            Console.ReadLine();
        }
    }

    class Person
    {
        public string GetName(ref string strSex)
        {
            if (strSex == "男")
            {
                strSex = "女";
                return "韩梅梅";
            }
            else
            {
                strSex = "男";
                return "李磊";
            }
        }
    }
}
