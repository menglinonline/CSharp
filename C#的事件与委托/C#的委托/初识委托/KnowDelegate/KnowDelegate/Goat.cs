using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowDelegate
{
    public class Goat
    {
        private string _name;

        public Goat(string name)
        {
            _name = name;
        }

        public void Eat(string food)
        {
            Console.WriteLine("{0}吃{1}", _name, food);
        }
    }
}
