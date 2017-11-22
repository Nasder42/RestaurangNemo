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
            string customerName  = "Default customer";
            int dishId;

            while (true)
            {
                byte[] bytes = new Byte[1024];
                int recBytes = _client.Receive(bytes); //Blocking
                if (recBytes == 0) break; // Checks if client its closed.
                // var nettoBytes = (bytes.Take(recBytes)).ToArray<byte>();
                string response = Encoding.UTF8.GetString(bytes, 0, recBytes);
                var tid = DateTime.Now.ToLongTimeString();
                var array = Encoding.ASCII.GetBytes(tid);
                _client.Send(array);

                switch (response)
                {
                    case "1":
                        dishId = 1;
                        DisplayInfo(tid, customerName, dishId);
                        break;
                    case "2":
                        dishId = 2;
                        DisplayInfo(tid, customerName, dishId);
                        break;

                    default:
                        break;
                }
            }
            _client.Close();
        }

        private void DisplayInfo(string tid, string customerName, int dishId)
        {
            Console.Write(tid + " - " + customerName + " - Maträtt: " + dishId);
        }
    }
}
