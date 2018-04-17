using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        //static byte[] data = new byte[1080];
        //static IAsyncResult result(TimeSpan a, Socket s, byte[] data)
        //{
        //    IAsyncResult result = s.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceivedData), s);
        //    return result;
        //}
        //static void ReceivedData(IAsyncResult iar)
        //{
        //    Socket remote = (Socket)iar.AsyncState;
        //    int recv = remote.EndReceive(iar);
        //    string receivedData = Encoding.ASCII.GetString(data, 0, recv);
        //}
        //static void Main(string[] args)
        //{
        //    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        //    Socket newsock = new Socket(AddressFamily.InterNetwork,
        //                    SocketType.Stream, ProtocolType.Tcp);
        //    TimeSpan time = TimeSpan.FromSeconds(1);

        //    try
        //    {
        //        newsock.Connect(ipep);
        //        Console.WriteLine("connect thanh cong voi: "+ipep);
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine("connect that bai");
        //    }

        //    while (true)
        //    {
        //        IAsyncResult result = newsock.BeginReceive(data, 0, data.Length, SocketFlags.None, null, null);
        //        result.AsyncWaitHandle.WaitOne(time);
        //        data = new byte[1024];
        //        string str = Console.ReadLine();
        //        data = Encoding.ASCII.GetBytes(str);
        //        newsock.Send(data, data.Length, SocketFlags.None);
        //        if (result.IsCompleted)
        //        {
        //            int rev = newsock.EndReceive(result);
        //            string str1 = Encoding.ASCII.GetString(data);
        //            Console.WriteLine(str1);
        //        }
        //    }
        //}
        static Socket sock;
        static void asd()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = sock.Receive(data, SocketFlags.None);
                string Data = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(Data.Substring(0, recv));
            }
        }
        static void Main(string[] args)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            byte[] data = new byte[1024];
            string stringData;
            int recv;
            sock.Connect(iep);
            Console.WriteLine("Connected to server at "+iep);
            recv = sock.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("Received: {0}", stringData);
            while (true)
            {
                Thread newthread = new Thread(new ThreadStart(asd));
                newthread.Start();
                stringData = Console.ReadLine();
                if (stringData == "exit")
                    break;
                data = Encoding.ASCII.GetBytes(stringData);
                sock.Send(data, data.Length, SocketFlags.None);
            }
            sock.Close();
        }
    }
}
