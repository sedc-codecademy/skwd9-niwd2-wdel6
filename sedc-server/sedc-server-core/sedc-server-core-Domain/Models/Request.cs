using System.Collections.Generic;
using System.Text;

namespace Sedc.Server.Core.sedc_server_core_Domain.Models
{
    internal class Request
    {
        public string Method { get; set; }
        public UrlAddress Address { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        private string HeaderString(int padBy = 0)
        {
            var sb = new StringBuilder();
            foreach (var item in Headers)
            {
                sb.Append($"{string.Empty.PadLeft(padBy, ' ')}{item.Key}: {item.Value}\r\n");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $@"Request:
  Method: {Method}
  Address: {Address}
  Headers: 
{HeaderString(4)}
";
        }
    }

}