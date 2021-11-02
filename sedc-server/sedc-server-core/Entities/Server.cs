using Sedc.Server.Core.Helpers.RequestHelpers;
using Sedc.Server.Core.Helpers.ResponseHelpers;
using Sedc.Server.Core.Helpers.ServerHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Entities
{
    public class Server
    {
        private readonly ServerOptions options;
        public Server(ServerOptions options = null)
        {
            if (options == null)
            {
                options = ServerOptions.Default;
            }
            this.options = options;
        }

        public void Start()
        {
            var address = IPAddress.Loopback;
            var port = options.Port;
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            Console.WriteLine($"Started listening on port {port}");

            while (true)
            {
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
