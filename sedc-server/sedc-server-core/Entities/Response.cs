using Sedc.Server.Core.Helpers.ResponseHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Entities
{
    internal class Response
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public Response()
        {
            Status = Status.ServerError;
        }
    }
}
