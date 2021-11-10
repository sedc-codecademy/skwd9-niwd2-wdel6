using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Logging.Interfaces;
using System;
using System.IO;
using System.Text;

namespace Sedc.Server.Core
{
    internal class RequestReader
    {

        private ILogger logger;
        public RequestReader(ILogger logger)
        {
            this.logger = logger;
        }

        internal Request ReadRequest(Stream stream)
        {
            byte[] bytes = new byte[1024];
            var readCount = stream.Read(bytes, 0, bytes.Length);
            string requestString = Encoding.ASCII.GetString(bytes, 0, readCount);
            logger.Debug($"Received request payload {requestString.Length} characters");
            var request = RequestParser.Parse(requestString, logger);
            logger.Debug($"Successfully parsed request");
            return request;
        }
    }
}