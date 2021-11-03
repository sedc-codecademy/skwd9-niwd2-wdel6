using System;

namespace Sedc.Server.Core
{
    internal class ResponseGenerator
    {

        private readonly IRequestProcessor processor;
        public ResponseGenerator(IRequestProcessor processor)
        {
            this.processor = processor;
        }

        internal IResponse GenerateResponse(Request request)
        {
            if (!ValidateRequest(request))
            {
                throw new ApplicationException("Validation failed");
                //return new Response { Message = "Invalid response"}
            }

            var response = processor.Process(request);
            return response;
        }

        private bool ValidateRequest(Request request)
        {
            return true;
        }
    }
}