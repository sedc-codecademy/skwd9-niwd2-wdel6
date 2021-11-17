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

        public void RegisterProcessor(IRequestProcessor processor) => this.options.RegisterProcessor(processor);

        public Server WithProcessor<T>(params object[] parameters) where T : IRequestProcessor
        {
            var processor = ServerHelper.ConstructProcessor<T>(parameters);
            RegisterProcessor(processor);
            return this;
        }

        public Server WithProcessor(IRequestProcessor processor)
        {
            RegisterProcessor(processor);
            return this;
        }

        public Server ServeStaticFiles(string basePath = "public-sedc") 
        {
            RegisterProcessor(new FileRequestProcessor(basePath));
            return this;
        }

        public ApiRequestProcessor ServeApi()
        {
            var result = new ApiRequestProcessor();
            result.LoadControllers();
            RegisterProcessor(result);
            return result;
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
