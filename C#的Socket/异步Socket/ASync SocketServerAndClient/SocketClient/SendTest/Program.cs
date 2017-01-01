using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SendTest
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BUFFER_SIZE = 1024;
        public byte[] buffer = new byte[BUFFER_SIZE];
        public StringBuilder sb = new StringBuilder();
    }

    class Program
    {
        static Socket clientSocket;
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8040)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            Send();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private static void  Send()
        {
            if (clientSocket != null)
            {
                try
                {
                    byte[] byteData = Encoding.UTF8.GetBytes("I need help server");
                    clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), clientSocket);
                    Console.WriteLine("BeginSend 执行完毕");
                    Console.ReadLine();
                }
                catch (Exception ex)
                { 
                    
                }
            }
        }

        /// <summary>
        /// 发送消息回掉函数
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                if (client != null)
                {
                    int bytesSent = client.EndSend(ar);
                    if (bytesSent > 0)
                    {
                        NegotiateReceive();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void NegotiateReceive()
        {
            try
            {
                if (clientSocket != null)
                {
                    StateObject state = new StateObject();
                    state.workSocket = clientSocket;

                    clientSocket.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(NegotiateReceiveCallBack), state);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private static void NegotiateReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    string msg = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);
                    Console.WriteLine("Server tell me：" + msg);
                }
            }
            catch (Exception ex)
            { 
                
            }
        }
    }
}
