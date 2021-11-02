using Sedc.Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Helpers.ResponseHelpers
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
            return new Response
            {
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
