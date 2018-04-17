using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
ProtocolType.Tcp);
            ipe = new IPEndPoint(IPAddress.Any, 9050);
            listen.Bind(ipe);
            listen.Listen(10);
            Console.WriteLine("Waiting for clients...");
            while (true)
            {
                if (listen.Poll(1000000, SelectMode.SelectRead))
                {
                    client = listen.Accept();
                    Thread newthread = new Thread(new ThreadStart(HandleConnection));
                    newthread.Start();
                }
            }

        }
        static void HandleConnection()
        {

        }
    }
}
