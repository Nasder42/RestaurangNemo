using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class SocketServer
    {
        Socket socket = SocketHelper.MySocket();
        public SocketServer()
        {
            Task task = new Task(() => Listen()); // Task task = new Task(Listen);  <-- Funkar också.
            task.Start();

            ShowServerEndPoint();

            //Listen();
            Console.WriteLine("Press a key to close the server");
            Console.ReadKey();
            socket.Close();

        }

        private void Listen()
        {
            bool serverOnline = true;
            while (serverOnline)
            {
                socket.Listen(10); // Lisen status
                Socket client = socket.Accept(); // Blocks here until recived.
                Client c = new Client(client);
                Console.WriteLine("Contact with: " + client.RemoteEndPoint + " stablished.");
                string welcome = "Welcome to Restaurang Nemo";
                byte[] toBytes = Encoding.ASCII.GetBytes(welcome);
                client.Send(toBytes);

                string meny = Meny();
                toBytes = Encoding.UTF8.GetBytes(meny);
                client.Send(toBytes);

                Console.ReadLine();
            }
        }

        private void ShowServerEndPoint()
        {
            Console.WriteLine("Address: {0}", ((IPEndPoint)socket.LocalEndPoint).Address);
            Console.WriteLine("Port: {0}", ((IPEndPoint)socket.LocalEndPoint).Port);
        }

        private string Meny()
        {
            string menu = "\r\n" +
                "Maträtter:" +
                "\r\n 1: Pasta" +
                "\r\n 2: Sallad" +
                "\r\n 3: Pizza" +
                "\r\n 4: Soppa \r\n";
            return menu;
        }
    }
}
