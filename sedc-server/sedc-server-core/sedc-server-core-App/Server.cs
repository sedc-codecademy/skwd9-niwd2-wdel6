using Sedc.Server.Core.sedc_server_core_App.Config;
using Sedc.Server.Core.sedc_server_core_BusinessLayer.Helpers;
using Sedc.Server.Core.sedc_server_core_BusinessLayer.Services.RequestService;
using Sedc.Server.Core.sedc_server_core_BusinessLayer.Services.ResponseService;
using System;
using System.Net;
using System.Net.Sockets;

namespace Sedc.Server.Core.sedc_server_core_App
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
