using System;

namespace Sedc.Server.Core
{
    internal class ResponseGenerator
    {
        public ResponseGenerator()
        {
        }

        internal Response GenerateResponse(Request request)
        {
            if (!ValidateRequest(request))
            {
                throw new ApplicationException("Validation failed");
                //return new Response { Message = "Invalid response"}
            }
            return new Response { Message = $"Hi, I'm a SEDC Server, nice to meet you! You used the {request.Method} method" };
        }

        private bool ValidateRequest(Request request)
        {
            return false;
        }
    }
}