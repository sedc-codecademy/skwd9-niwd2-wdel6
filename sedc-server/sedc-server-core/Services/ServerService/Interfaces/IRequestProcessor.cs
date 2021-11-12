using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Logging;
using Sedc.Server.Core.Logging.Interfaces;
using Sedc.Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Services.RequestService.Interfaces
{
    public interface IRequestProcessor
    {
        IResponse Process(Request request, ILogger logger);

        bool ShouldProcess(Request request);

        string Describe() => GetType().FullName;
    }
}
