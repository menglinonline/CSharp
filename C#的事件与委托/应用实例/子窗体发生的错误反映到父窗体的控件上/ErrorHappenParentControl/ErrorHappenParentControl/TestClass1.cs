using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class TestClass1
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

        public void Test()
        {
            TestClass2 testClass2 = new TestClass2();
            testClass2.OnError += testClass2_OnError;

            testClass2.ConvertToInt();
        }

        void testClass2_OnError(Exception e, string errorMsg, DateTime time)
        {
            this.Error(e, errorMsg);
        }
    }
}
