using System;

namespace Sedc.Server.Core
{
    internal class ResponseGenerator
    {

        private readonly IRequestProcessor processor;
        private readonly ILogger logger;
        public ResponseGenerator(IRequestProcessor processor, ILogger logger)
        {
            this.processor = processor;
            this.logger = logger;
        }

        internal IResponse GenerateResponse(Request request)
        {
            if (!ValidateRequest(request))
            {
                throw new ApplicationException("Validation failed");
                //return new Response { Message = "Invalid response"}
            }
            logger.Debug($"Running {processor.Describe()}");
            var response = processor.Process(request, logger);
            return response;
        }

        private bool ValidateRequest(Request request)
        {
            return true;
        }
    }
}