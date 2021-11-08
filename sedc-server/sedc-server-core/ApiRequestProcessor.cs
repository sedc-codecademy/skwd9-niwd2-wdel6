using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class ApiRequestProcessor : IRequestProcessor
    {
        public string Describe() => "ApiRequestProcessor";

        public IResponse Process(Request request, ILogger logger)
        {
            return new JsonResponse<int>
            {
                Message = 4,
                Status = Status.OK
            };
        }

        public bool ShouldProcess(Request request)
        {
            if (request.Address.Path.Count() == 0) {
                return false;
            }
            var prefix = request.Address.Path.First();
            return (prefix == "api");
        }
    }
}
