using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicPolymorphic
{
    class Program
    {
        static void Main(string[] args)
        {
            Human human1 = new Man();
            human1.CleanRoom();
            Human human2 = new Woman();
            human2.CleanRoom();

            Console.ReadLine();
        }
    }

    class Human
    {
        public virtual void CleanRoom()
        {
            Console.WriteLine("Human clean room");
        }
    }

    class Man : Human
    {
        public override void CleanRoom()
        {
            Console.WriteLine("Man clean room slowly");
        }
    }

    class Woman : Human
    {
        public override void CleanRoom()
        {
            Console.WriteLine("Woman clean room quickly");
        }
    }
}
