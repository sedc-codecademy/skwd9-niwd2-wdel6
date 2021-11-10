﻿using Sedc.Server.Core.Services.RequestService.Interfaces;
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
            options = ServerOptions.MergeWithDefault(options);
            this.options = options;
        }

        public void RegisterProcessor(IRequestProcessor processor) => this.options.RegisterProcessor(processor);

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
                var generator = new ResponseGenerator(options.Processors, options.Logger);
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
