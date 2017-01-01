using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建TestClass类的对象 
            TestClass testClass = new TestClass();

            //在testclass对象的mainThread(委托)对象上搭载两个方法，在线程中调用mainThread对象时相当于调用了这两个方法。 
            testClass.mainThread = new TestClass.testDelegate(refreshLabMessage1);
            testClass.mainThread += new TestClass.testDelegate(refreshLabMessage2);

            //创建一个无参数的线程,这个线程执行TestClass类中的testFunction方法。 
            Thread testclassThread = new Thread(new ThreadStart(testClass.testFunction));
            //启动线程，启动之后线程才开始执行 
            testclassThread.Start();
        }

        /// <summary> 
        /// 在界面上更新线程执行次数 
        /// </summary> 
        /// <param name="i"></param> 
        private void refreshLabMessage1(long i)
        {
            //判断该方法是否被主线程调用，也就是创建labMessage1控件的线程，当控件的InvokeRequired属性为ture时，说明是被主线程以外的线程调用。如果不加判断，会造成异常 
            if (this.labMessage1.InvokeRequired)
            {
                //再次创建一个TestClass类的对象 
                TestClass testclass = new TestClass();
                //为新对象的mainThread对象搭载方法 
                testclass.mainThread = new TestClass.testDelegate(refreshLabMessage1);
                //this指窗体，在这调用窗体的Invoke方法，也就是用窗体的创建线程来执行mainThread对象委托的方法，再加上需要的参数(i) 
                this.Invoke(testclass.mainThread, new object[] { i });
            }
            else
            {
                labMessage1.Text = i.ToString();
            }
        }


        /// <summary> 
        /// 在界面上更新线程执行次数 
        /// </summary> 
        /// <param name="i"></param> 
        private void refreshLabMessage2(long i)
        {
            //同上 
            if (this.labMessage2.InvokeRequired)
            {
                //再次创建一个TestClass类的对象 
                TestClass testclass = new TestClass();
                //为新对象的mainThread对象搭载方法 
                testclass.mainThread = new TestClass.testDelegate(refreshLabMessage2);
                //this指窗体，在这调用窗体的Invoke方法，也就是用窗体的创建线程来执行mainThread对象委托的方法，再加上需要的参数(i) 
                this.Invoke(testclass.mainThread, new object[] { i });
            }
            else
            {
                labMessage2.Text = i.ToString();
            }
        }
    }
}
