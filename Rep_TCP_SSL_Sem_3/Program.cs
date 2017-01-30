using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Rep_TCP_SSL_Sem_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string certificateServerName = "FakeServerName ";
            bool leaveInnerStreamOpen = false;

            TcpClient clientSocket = new TcpClient("localhost", 6789);
            Stream unsecureStream = clientSocket.GetStream();

            SslStream sslStream = new SslStream(unsecureStream, leaveInnerStreamOpen);
            sslStream.AuthenticateAsClient(certificateServerName);

            StreamReader sr = new StreamReader(sslStream);
            StreamWriter sw = new StreamWriter(sslStream);
            sw.AutoFlush = true;

            for (int i = 0; i < 5; i++)
            {
                string message = Console.ReadLine();
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();
                Console.WriteLine("Server: " + serverAnswer);
            }
            sslStream.Close();
            clientSocket.Close();
        }
    }
}
