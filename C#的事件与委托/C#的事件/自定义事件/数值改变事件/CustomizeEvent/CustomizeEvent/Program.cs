using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomizeEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomizeEvent c = new CustomizeEvent(5);
            c.SetValue(6);
            //
            c.ChangeNum += new CustomizeEvent.NumMainpulationHandler(CustomizeEvent.EventFailed);
            c.SetValue(7);
            Console.ReadLine();
        }
    }

    class CustomizeEvent
    {
        public CustomizeEvent(int n)
        {
            SetValue(n);
        }

        private int value;
        //数值一旦被操作就会触发这么一个delegate
        public delegate void NumMainpulationHandler();
        //哪种类型的delegate可以绑定到这个event上面(delegate是在event里面声明的)
        public event NumMainpulationHandler ChangeNum;

        //当数字变动时就会调用这么一个方法
        private void NumChanged()
        {
            //event是否有delegate绑定上去了，绑定上去了就会触发delegate，没有输出一句话
            if (ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("Event Failed");
            }
        }

        //什么样的操作会触发这个事件呢，设值的时候
        public void SetValue(int n)
        {
            if(value != n)
            {
                value = n;
                NumChanged();
            }
        }

        public static void EventFailed()
        {
            Console.WriteLine("Bingding Event Failed");
        }
    }
}
