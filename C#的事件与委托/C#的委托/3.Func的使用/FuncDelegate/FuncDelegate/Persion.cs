using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDelegate
{
    public class Persion
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Persion GetPersionInfo()
        {
            //消除重复的try catch代码
            //try
            //{
            //    Persion persion = new Persion();
            //    persion.Name = "David";
            //    persion.Age = 30;

            //    return persion;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}

            return Utillity.TryExecute<Persion>(() =>
            {
                Persion persion = new Persion();
                persion.Name = "David";
                persion.Age = 30;

                return persion;
            }
           , "GetPersionInfo");
        }
    }
}
