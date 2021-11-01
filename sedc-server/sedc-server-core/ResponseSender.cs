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
            // var statusLine = "HTTP/1.1 "+ (int)response.Status + " " +response.Message + "\r\n"; // UGLY - NEVER DO THIS, NOT EVEN IN C# 1.0
            // var statusLine = string.Format("HTTP/1.1 {0} {1}\r\n", (int)response.Status, response.Message); // MUCH BETTER - ONLY UP TO C# 6.0
            var statusLine = $"HTTP/1.1 {response.Status.Code} {response.Status.Message}\r\n";

            var separator = "\r\n";
            var body = response.Message;

            var responseString = $"{statusLine}{separator}{body}";

            var responseBytes = Encoding.ASCII.GetBytes(responseString);
            stream.Write(responseBytes);
        }
    }
}