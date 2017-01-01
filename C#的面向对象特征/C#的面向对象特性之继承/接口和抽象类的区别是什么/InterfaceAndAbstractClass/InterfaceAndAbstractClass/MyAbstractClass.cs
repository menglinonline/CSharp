using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAndAbstractClass
{
    public abstract class MyAbstractClass
    {
        /// <summary>
        /// 抽象类中对于不是抽象的方法必须提供成员的实现细节
        /// </summary>
        public void ListenMusic()
        {
            Console.WriteLine("ListenMusic");
        }

        public abstract void LookBook();
    }
}
