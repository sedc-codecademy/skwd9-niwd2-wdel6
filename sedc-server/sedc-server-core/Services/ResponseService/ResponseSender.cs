using Sedc.Server.Core.Helpers.CustomExceptions;
using Sedc.Server.Core.Response;
using Sedc.Server.Core.Response.Implementations;
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

        internal void Send(IResponse response, NetworkStream stream)
        {
            // var statusLine = "HTTP/1.1 "+ (int)response.Status + " " +response.Message + "\r\n"; // UGLY - NEVER DO THIS, NOT EVEN IN C# 1.0
            // var statusLine = string.Format("HTTP/1.1 {0} {1}\r\n", (int)response.Status, response.Message); // MUCH BETTER - ONLY UP TO C# 6.0
            var statusLine = $"HTTP/1.1 {response.Status.Code} {response.Status.Message}\r\n";

            var separator = "\r\n";

            if (response is TextResponse textResponse)
            {
                var body = textResponse.Message;
                var responseString = $"{statusLine}{separator}{body}";
                var responseBytes = Encoding.ASCII.GetBytes(responseString);
                stream.Write(responseBytes);
            } 
            else if (response is BinaryResponse binaryResponse)
            {
                var responseString = $"{statusLine}{separator}";
                var responseBytes = Encoding.ASCII.GetBytes(responseString);
                stream.Write(responseBytes);
                var body = binaryResponse.Message;
                stream.Write(body);
            } 
            else if (response is JsonResponse jsonResponse)
            {
                var responseString = $"{statusLine}{separator}";
                var responseBytes = Encoding.ASCII.GetBytes(responseString);
                stream.Write(responseBytes);
                var body = Encoding.ASCII.GetBytes(jsonResponse.GetMessagePayload());
                stream.Write(body);
            } 
            else
            {
                throw new SedcServerException($"Invalid response type {response.GetType().FullName}");
            }

        }
    }
}