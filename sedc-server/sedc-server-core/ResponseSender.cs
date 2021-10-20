using System;
using System.Net.Sockets;
using System.Text;

namespace Sedc.Server.Core
{
    internal class ResponseSender
    {
        public ResponseSender()
        {
        }

        internal void Send(Response response, NetworkStream stream)
        {
            var statusLine = "HTTP/1.1 200 OK\r\n";
            var separator = "\r\n";
            var body = response.Message;

            var responseString = $"{statusLine}{separator}{body}";

            var responseBytes = Encoding.ASCII.GetBytes(responseString);
            stream.Write(responseBytes);
        }
    }
}