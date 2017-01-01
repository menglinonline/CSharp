using EncapsulationAnother;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //public,private,internal,protected,internal protected
            Persion persion = new Persion();
            Console.WriteLine(persion.GetAge());

            //访问另一个命名空间 internal
            AnotherNamespaceClass anc = new AnotherNamespaceClass();
            Console.WriteLine(anc.internalString);

            //protected
            Man man = new Man();
            Console.WriteLine(man.GetPersionHeight());

            
            Console.ReadLine();
        }
    }

    class Persion
    {
        public int age;
        private string name;

        protected string GetHeight()
        { 
             return "170cm";
        }

        public int GetAge()
        {
            if (CheckAge())
            {
                return age;
            }
            return -1;
        }

        private bool CheckAge()
        {
            if (age <= 0)
            {
                return false;
            }

            return true;
        }

        private string GetName()
        {
            return name;
        }
    }

    class Man : Persion
    {
        public string GetPersionHeight()
        {
            return base.GetHeight();
        }
    }
}
