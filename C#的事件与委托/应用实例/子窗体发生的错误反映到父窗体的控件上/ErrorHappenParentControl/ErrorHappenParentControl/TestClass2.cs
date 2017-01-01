using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class TestClass2
    {
        public delegate void ErrorCallBack(Exception e, string errorMsg, DateTime time);
        public event ErrorCallBack OnError;

        public void Error(Exception ex, string message)
        {
            if (OnError != null)
            {
                OnError(ex, message, DateTime.Now);
            }
        }

        public void ConvertToInt()
        {
            try
            {
                int i = Convert.ToInt16("ABC");
            }
            catch (Exception ex)
            {
                Error(ex, "转化错误");
            }
        }
    }
}
