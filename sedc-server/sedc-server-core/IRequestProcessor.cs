using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public interface IRequestProcessor
    {
        IResponse Process(Request request, ILogger logger);

        bool ShouldProcess(Request request);

        string Describe() => GetType().FullName;
    }
}
