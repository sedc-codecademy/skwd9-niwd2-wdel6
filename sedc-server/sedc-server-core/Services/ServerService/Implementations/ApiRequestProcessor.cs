using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Logging;
using Sedc.Server.Core.Logging.Interfaces;
using Sedc.Server.Core.Response;
using Sedc.Server.Core.Response.Implementations;
using Sedc.Server.Core.Services.RequestService.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Services.RequestService.Implementations
{
    public class ApiRequestProcessor : IRequestProcessor
    {
        public string Describe() => "ApiRequestProcessor";

        public string Prefix { get; private set; }
        public IApiController Controller { get; private set; }
        public ApiRequestProcessor(string prefix = "api")
        {
            Prefix = prefix;
            Controller = new DefaultApiController();
        }

        public ApiRequestProcessor(IApiController controller, string prefix = "api") : this(prefix)
        {
            Controller = controller;
        }


        public ApiRequestProcessor WithController(IApiController controller)
        {
            Controller = controller;
            return this;
        }

        public IResponse Process(Request request, ILogger logger)
        {
            var path = request.Address.Path.Skip(1);
            var parameters = request.Address.Params;
            var method = request.Method;

            try {
                var result = Controller.Execute(path, parameters, method, logger);

                return new JsonResponse<object>
                {
                    Message = result,
                    Status = Status.OK
                };
            } catch (Exception ex) {
                throw new SedcServerException($"Error executing {Controller.Name}", ex);
            }
        }

        public bool ShouldProcess(Request request)
        {
            if (request.Address.Path.Count() == 0)
            {
                return false;
            }
            var prefix = request.Address.Path.First();
            return prefix == Prefix;
        }
    }
}
