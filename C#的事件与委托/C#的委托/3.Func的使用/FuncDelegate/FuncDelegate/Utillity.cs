using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncDelegate
{
    public class Utillity
    {
        public static T TryExecute<T>(Func<T> func, string methodName)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MethodName:" + methodName + " Error:" + ex.Message);
                return default(T);
            }
        }
    }
}
