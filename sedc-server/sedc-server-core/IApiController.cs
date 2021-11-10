using System.Collections.Generic;

namespace Sedc.Server.Core
{
    public interface IApiController
    {
        object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger);
    }

}