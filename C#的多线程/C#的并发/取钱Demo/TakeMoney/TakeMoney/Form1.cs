using Kevin.Comm.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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

        public int bankCardBlance = 5000;//银行卡余额
      
        private void button1_Click(object sender, EventArgs e)
        {
            bankCardBlance =  Convert.ToInt16(this.txtBankCardBalance.Text);//设置银行卡余额

            int threadCount = Convert.ToInt16(this.txtThreadCount.Text);//设置线程数(相当于多人取钱)
            //声明多个线程，相当于多个人同时去取钱
            Thread[] threads = new Thread[threadCount];
            //Create multi thread
            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(TakeMoney));
                thread.Name = "TakeMoney" + i;
                threads[i] = thread;
            }

            //Multi thread start
            for (int i = 0; i < threadCount; i++)
            {
                //开始执行TakeMoney()
                //连着取多次500元现金，那么就目前来说，账户总额为5000元，只能取十次就不能取了，账户为0了，金额就不应该在减少了
                List<string> arrayList = new List<string>();
                arrayList.Add(this.txtTakeMoney.Text);
                arrayList.Add(threads[i].Name);
                threads[i].Start(arrayList);
            }
        }

        private static object lockObj = new object();
        /// <summary>
        /// 取钱
        /// </summary>
        /// <param name="parameterObject">要取走的金额和线程名称</param>
        /// <returns></returns>
        public void TakeMoney(object parameterObject)
        {
            List<string> parameterList = parameterObject as List<string>;
            int takeMoney = Convert.ToInt16(parameterList[0]);
            string threadName =  parameterList[1];
           
            //可以注释lock看看效果
            lock (lockObj)//此处用typeof(Thread)效果一样
            {
                //如果银行的钱大于等于要取走的金额，则减少钱
                if (bankCardBlance >= takeMoney)
                {
                    Thread.Sleep(5);
                    bankCardBlance = bankCardBlance - takeMoney;
                    Tracer.TraceLog(threadName + "取走了 " + takeMoney, "TakeMoneyInfo", "Info", true);
                }
                else if (this.bankCardBlance <= 0)
                {
                    Tracer.TraceLog(threadName + "对不起，已经没有钱了", "TakeMoneyInfo", "Info", true);
                }
            }
        }
    }
}
