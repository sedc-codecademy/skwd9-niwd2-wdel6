using System.Collections.Generic;

namespace Sedc.Server.Core
{
    public interface IApiController
    {
        string Name { get; }
        object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger);
    }

}