using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class BinaryResponse : IResponse<byte[]>
    {
        public Status Status { get; set; }
        public byte[] Message { get; set; }

        public BinaryResponse()
        {
            Status = Status.OK;
        }
    }
}
