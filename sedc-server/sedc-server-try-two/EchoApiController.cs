using Sedc.Server.Core;

using System.Collections.Generic;
using System.Linq;

namespace Sedc.Server.TryTwo
{
    internal class EchoApiController : IApiController
    {
        public EchoApiController(string problem)
        {

        }

        public string Name { get => nameof(EchoApiController); }

        public object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger)
        {
            string paramString = string.Join(" / ", parameters.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            logger.Debug($"Path: {string.Join(" / ", path)}");
            logger.Debug($"Params: {paramString}");
            logger.Debug($"Method: {method}");

            path = path.Skip(1);
            string pathString = string.Join(" / ", path);
            return new
            {
                Path = pathString,
                Params = paramString,
                Method = method
            };
        }
    }
}