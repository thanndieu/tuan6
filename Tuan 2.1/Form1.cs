using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Tuan_2._1
{
    public partial class Form1 : Form
    {
        IPEndPoint ipep;
        Socket newsock;
        int recv;
        byte[] data = new byte[1024];
        Socket client;
        static int connections = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(textBox2.Text);
            client.Send(data, data.Length, SocketFlags.None);
            listBox1.Items.Add(textBox2.Text.ToString());
            Thread newthread = new Thread(new ThreadStart(asd));
            newthread.Start();

                
            
           


        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ipep = new IPEndPoint(IPAddress.Any, 9050);
            newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);

            listBox1.Items.Add("Waiting for clients..."+connections);
            client = newsock.Accept();

            textBox1.Text = "number of clients connection 1";

        }
        public void asd()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = client.Receive(data, SocketFlags.None);
                    string Data = Encoding.ASCII.GetString(data, 0, recv);
                  this.Invoke(new MethodInvoker(delegate()
                {
                    listBox1.Items.Add(Data.Substring(0,recv));
                }));
            }
        }
    }
}
