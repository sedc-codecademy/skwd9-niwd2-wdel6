using Sedc.Server.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    internal static class ApiRequestProcessorHelper
    {
        public static void LoadControllers(ApiRequestProcessor processor)
        {
            var allTypes = Assembly.GetEntryAssembly().GetTypes();
            foreach (var type in allTypes)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Contains(typeof(IApiController)))
                {
                    var parameterlessConstructor = type.GetConstructor(new Type[0]);
                    if (parameterlessConstructor != null)
                    {
                        //var controller = parameterlessConstructor.Invoke(new object[0]);
                        // processor.WithController(controller as IApiController);
                        processor.WithController(type);
                    }
                    else
                    {
                        //Console.WriteLine($"Found {type.FullName} but without a parameterless constructor");
                        //Console.WriteLine($"   Maybe we need to inject dependencies?");
                    }
                }
            }
        }

        public static string GetControllerRoute(Type controllerType)
        {
            var apiRoutes = controllerType.GetCustomAttributes<ApiRouteAttribute>();
            var apiRoute = apiRoutes.FirstOrDefault();
            if (apiRoute == null)
            {
                var name = controllerType.Name;
                if (name.EndsWith("ApiController"))
                {
                    return name.Substring(0, name.IndexOf("ApiController")).ToLowerInvariant();
                }
                return name.ToLowerInvariant();
            }
            return apiRoute.Route.ToLowerInvariant();
        }

        internal static IApiController ContructController(Type controllerType)
        {
            var controller = controllerType.GetConstructor(new Type[0]).Invoke(new object[0]) as IApiController;
            return controller;
        }
    }
}
