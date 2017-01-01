using System;
using System.Collections.Generic;
using System.IO;
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
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
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

            var au = new ArgumentException();//参数异常
            var aun = new ArgumentNullException();//参数为空异常
            var aunor = new ArgumentOutOfRangeException();//参数超出范围异常
            var dne = new DirectoryNotFoundException();//路径没有找到异常
            var fne = new FileNotFoundException();//文件没有找到异常
            var ioe = new InvalidOperationException();//非法运算符异常
            var nie = new NotImplementedException();//未实现异常
        }
    }
}
