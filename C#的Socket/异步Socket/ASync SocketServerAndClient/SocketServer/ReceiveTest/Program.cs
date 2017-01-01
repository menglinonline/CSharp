using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveTest
{
    class Program
    {
        private static int myProt = 8040;   //端口
        static Socket serverSocket;
        static Socket acceptSocket;
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            Thread myThread = new Thread(Receive);
            acceptSocket = serverSocket.Accept();

            myThread.Start();
            Console.ReadLine();
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private static void Receive()
        {
            if (acceptSocket != null)
            {
                try 
                {
                    StateObject state = new StateObject();
                    state.workSocket = acceptSocket;
                    acceptSocket.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReceiveCallback), state);
                    Console.WriteLine("BeginReceive 执行完毕");
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 接收消息回掉函数
        /// </summary>
        /// <param name="ar"></param>
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject so = (StateObject)ar.AsyncState;
                Socket s = so.workSocket;
                int read = s.EndReceive(ar);
                if (read > 0)
                {
                    string msg = Encoding.UTF8.GetString(so.buffer, 0, read);
                    Console.WriteLine("接收来自客户端的消息：{0}", msg);

                    //回复
                    Response();                  
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 回复
        /// </summary>
        private static void Response()
        {
            byte[] byteData = Encoding.UTF8.GetBytes("Hello client,I know I came over");
            acceptSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(ResponseCallBack), serverSocket);
        }

        /// <summary>
        /// 回复的回调函数
        /// </summary>
        private static void ResponseCallBack(IAsyncResult ar)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }
        }

        public class StateObject
        {
            public Socket workSocket = null;
            public const int BUFFER_SIZE = 1024;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public StringBuilder sb = new StringBuilder();
        }
    }
}
