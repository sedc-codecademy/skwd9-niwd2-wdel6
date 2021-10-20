using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace sedc_server_try_one
{
    internal class ResponseSender
    {
        public ResponseSender()
        {
        }

        internal void Send(Response response, Stream stream)
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