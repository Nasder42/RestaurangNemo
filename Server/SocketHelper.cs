using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class SocketHelper
    {
        public static EndPoint MyEndPoint()
        {
            // TODO: Adaptera nurvarande local ip.
            IPAddress adr = IPAddress.Parse("127.0.0.1");
            int port = 8080;
            IPEndPoint endPoint = new IPEndPoint(adr, port);

            return endPoint;
        }

        //public static EndPoint SecondEndPoint()
        //{
        //    IPAddress adr = IPAddress.Any;
        //    int port = 8080;
        //    IPEndPoint endPoint = new IPEndPoint(adr, port);
        //    return endPoint;
        //}

        // Skapa en ip4 TCP-Socket
        public static Socket MySocket()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(MyEndPoint());
            return socket;
        }

    }
}
