﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketServer
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
        private static byte[] result = new byte[1024];
        private static int myProt = 8040;   //端口
        static Socket serverSocket;
        static void Main(string[] args)
        {
            //服务器IP地址
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求

            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //通过Clientsoket发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                //clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);
                    Console.WriteLine("接收来自客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(result, 0, receiveNumber));

                    myClientSocket.Send(Encoding.ASCII.GetBytes("Hello client,I know I came over. "));
                
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
    }
}
