using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        Socket _client;
        Task _task;

        public Client(Socket client)
        {
            _client = client;
            _task = new Task(Listen);
            _task.Start();
        }

        public void Listen()
        {
            string line = "";
            while (true)
            {
                byte[] bytes = new Byte[1024];
                int recBytes = _client.Receive(bytes); //Blocking
                if (recBytes == 0) break; // Checks if client its closed.
                // var nettoBytes = (bytes.Take(recBytes)).ToArray<byte>();
                string response = Encoding.UTF8.GetString(bytes, 0, recBytes);

                switch (response)
                {
                    case "1":
                        Console.WriteLine("Maträtt Pasta beställt.");
                        break;
                    case "2":
                        Console.WriteLine("Maträtt 2 beställt.");
                        break;
                }

                //if (response.ToLower() == "x")
                //{
                //    Console.WriteLine("Client # closed.");
                //    break;
                //}
                if (response == "\r\n")
                {
                    var tid = DateTime.Now.ToLongTimeString();
                    var array = Encoding.ASCII.GetBytes(tid);
                    _client.Send(array);

                    Console.Write(tid + "- Maträtt: ");
                    Console.WriteLine(line);
                    line = "";
                }
                else
                {
                    line += response;
                }
            }
            _client.Close();
        }
    }
}
