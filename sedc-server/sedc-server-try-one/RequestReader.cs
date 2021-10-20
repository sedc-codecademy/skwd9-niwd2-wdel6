using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace sedc_server_try_one
{
    public class RequestReader
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
            Console.WriteLine(request);
            return request;
        }
    }
}
