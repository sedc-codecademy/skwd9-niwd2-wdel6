using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    class DefaultApiController : IApiController
    {
        public object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger)
        {
            return new
            {
                First = 1,
                Second = 3,
                Operation = "Add",
                Result = 4
            };
        }
    }
}
