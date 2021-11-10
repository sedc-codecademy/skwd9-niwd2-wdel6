using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Response.Implementations
{
    public class TextResponse : IResponse<string>
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public TextResponse()
        {
            Status = Status.OK;
        }
    }
}
