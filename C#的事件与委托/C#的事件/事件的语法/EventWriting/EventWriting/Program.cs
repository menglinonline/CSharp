using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventWriting
{
    class Program
    {
        static void Main(string[] args)
        {
            EventTest eventTest = new EventTest(3);
            eventTest.SetValue(5);
            eventTest.ChangeNum += new EventTest.NumOperateHandler(EventTest.BindingEvent);
            eventTest.SetValue(8);
            Console.ReadLine();
        }
    }

    class EventTest
    {
        private int value;
        //数值一操作就会触发这么一个委托
        public delegate void NumOperateHandler();
        //数字改变事件
        public event NumOperateHandler ChangeNum;

        /// <summary>
        /// 在它初始化的时候就要设值
        /// </summary>
        /// <param name="v"></param>
        public EventTest(int v)
        {
            SetValue(v);
        }

        private void OnNumChanged()
        {
            //ChangeNum Event是否有委托绑定上去了
            if (ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("Event failed");
            }
        }

        /// <summary>
        /// 什么样的操作会触发这个事件呢？当设值的时候
        /// </summary>
        /// <param name="v"></param>
        public void SetValue(int v)
        {
            if (value != v)
            {
                value = v;
                OnNumChanged();
            }
        }

        public static void BindingEvent()
        {
            Console.WriteLine("Binding event success");
        }
    }
}
