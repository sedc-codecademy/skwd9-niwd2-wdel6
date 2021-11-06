using System;
using System.Net;
using System.Net.Sockets;

namespace Sedc.Server.Core
{
    public class Server
    {
        private readonly ServerOptions options;
        public Server(ServerOptions options = null)
        {
            options = ServerOptions.MergeWithDefault(options);
            this.options = options;
        }

        public void Start()
        {
            var address = IPAddress.Loopback;
            var port = options.Port; 
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            options.Logger.Info($"Started listening on port {port}");

            while (true)
            {
                // Accept the connection
                options.Logger.Debug($"Waiting for tcp client");
                var client = listener.AcceptTcpClient();
                options.Logger.Debug($"Accepted tcp client");
                var stream = client.GetStream();

                // Process the request
                var reader = new RequestReader(options.Logger);
                var request = reader.ReadRequest(stream);

                // Generate a response based on the request
                var generator = new ResponseGenerator(options.Processor, options.Logger);
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
