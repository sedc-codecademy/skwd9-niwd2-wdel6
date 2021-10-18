using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace sedc_server_try_one
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Loopback;
            var port = 664; // The Neighbour of the Beast
            TcpListener listener = new TcpListener(address, port);

            listener.Start();
            Console.WriteLine($"Started listening on port {port}");

            var client = listener.AcceptTcpClient();
            Console.WriteLine($"Accepted tcp client");

            var stream = client.GetStream();

            byte[] bytes = new byte[1024];
            var readCount = stream.Read(bytes, 0, bytes.Length);
            Console.WriteLine($"Read {readCount} bytes");

            // string byteString = string.Join(",", bytes.Select(b => (char)b).Take(readCount));

            string result = Encoding.ASCII.GetString(bytes, 0, readCount);

            Console.WriteLine(result);
        }
    }
}
