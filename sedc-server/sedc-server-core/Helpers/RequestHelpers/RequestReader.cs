using Sedc.Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Helpers.RequestHelpers
{
    internal class RequestReader
    {
        public RequestReader()
        {
        }

        internal Request ReadRequest(Stream stream)
        {
            byte[] bytes = new byte[1024];
            var readCount = stream.Read(bytes, 0, bytes.Length);
            string requestString = Encoding.ASCII.GetString(bytes, 0, readCount);
            var request = RequestParser.Parse(requestString);
            return request;
        }
    }
}
