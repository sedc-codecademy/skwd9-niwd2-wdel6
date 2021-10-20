using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace sedc_server_try_one
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Loopback;
            var port = 664; // The Neighbour of the Beast
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            Console.WriteLine($"Started listening on port {port}");

            while (true) {
                // Accept the connection
                Console.WriteLine($"Waiting for tcp client");
                var client = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted tcp client");
                var stream = client.GetStream();

                // Process the request
                var reader = new RequestReader();
                var request = reader.ReadRequest(stream);

                // Generate a response based on the request

                var generator = new ResponseGenerator();
                var response = generator.GenerateResponse(request);

                // Sending the response
                var sender = new ResponseSender();
                sender.Send(response, stream);

                // close the connection
                client.Close();
            }
        }
    }
}
