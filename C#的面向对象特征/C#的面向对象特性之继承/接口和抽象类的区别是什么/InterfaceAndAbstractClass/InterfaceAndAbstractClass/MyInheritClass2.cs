using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAndAbstractClass
{
    class MyInheritClass2:MyAbstractClass
    {
        /// <summary>
        /// 使用override关键字覆盖abstract方法
        /// </summary>
        public override void LookBook()
        {
            Console.WriteLine("ListenMusic");
        }
    }
}
