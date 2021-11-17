using Sedc.Server.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class ApiRequestProcessor : IRequestProcessor
    {
        public string Describe() => "ApiRequestProcessor";

        public string Prefix { get; private set; }

        //private Dictionary<string, IApiController> controllers = new Dictionary<string,IApiController>();

        private Dictionary<string, Type> controllerTypes = new Dictionary<string, Type>();

        // public IEnumerable<IApiController> Controllers { get => controllers.Values; }
        public IEnumerable<Type> Controllers { get => controllerTypes.Values; }

        public ApiRequestProcessor(string prefix = "api")
        {
            Prefix = prefix;
        }

        public ApiRequestProcessor LoadControllers()
        {
            ApiRequestProcessorHelper.LoadControllers(this);
            return this;
        }

        //public ApiRequestProcessor WithController(IApiController controller)
        //{
        //    var route = ApiRequestProcessorHelper.GetControllerRoute(controller.GetType());
        //    controllers.Add(route, controller);
        //    return this;
        //}

        public ApiRequestProcessor WithController<T>() where T : IApiController, new()
        {
            var route = ApiRequestProcessorHelper.GetControllerRoute(typeof(T));
            //var controller = new T();
            //controllers.Add(route, controller);
            controllerTypes.Add(route, typeof(T));
            return this;
        }

        internal ApiRequestProcessor WithController(Type type)
        {
            // code that makes sure we call it only with IApiController types
            Debug.Assert(type.IsAssignableTo(typeof(IApiController)));

            var route = ApiRequestProcessorHelper.GetControllerRoute(type);
            controllerTypes.Add(route, type);
            return this;
        }

        public IResponse Process(Request request, ILogger logger)
        {
            var path = request.Address.Path.Skip(1);
            var parameters = request.Address.Params;
            var method = request.Method;

            var controllerRoute = path.FirstOrDefault();
            if (controllerRoute == null)
            {
                return NotFound();
            }

            if (!controllerTypes.TryGetValue(controllerRoute.ToLowerInvariant(), out var controllerType))
            {
                return NotFound();
            };

            try {
                var controller = ApiRequestProcessorHelper.ContructController(controllerType);
                var result = controller.Execute(path, parameters, method, logger);

                return new JsonResponse<object>
                {
                    Message = result,
                    Status = Status.OK
                };
            } catch (Exception ex) {
                throw new SedcServerException($"Error executing {controllerType.Name}", ex);
            }
        }

        private static IResponse NotFound()
        {
            return new TextResponse
            {
                Message = "Not Found",
                Status = Status.NotFound
            };
        }

        public bool ShouldProcess(Request request)
        {
            if (!request.Address.Path.Any()) {
                return false;
            }
            var prefix = request.Address.Path.First();
            return prefix == Prefix;
        }
    }
}
