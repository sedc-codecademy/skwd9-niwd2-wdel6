using Sedc.Server.Core.sedc_server_core_Domain.Models;
using System;

namespace Sedc.Server.Core.sedc_server_core_BusinessLayer.Helpers
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
            return new Response { 
                Message = $"Hi, I'm a SEDC Server, nice to meet you! You used the {request.Method} method",
                Status = Status.OK
            };
        }

        private bool ValidateRequest(Request request)
        {
            return true;
        }
    }
}