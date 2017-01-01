using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            //int x = 0;
            //int y = 100 / x;

            try
            {
                int x = 0;
                int y = 100 / x;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("finally");
            }

            //throw new NullReferenceException();
        }
    }
}
