
using Sedc.Server.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sedc_server_try_two
{
    class MyRequestProcessor : IRequestProcessor
    {
        public IResponse Process(Request request, ILogger logger)
        {
            return new TextResponse
            {
                Message = $@"
<html>
    <head>
        <title>Custom SEDC Server</title>
    </head>
    <body>
        Hi, I'm a <b>Custom SEDC Server</b> and <i>I don't like you</i>! You used the {request.Method} method on the {request.Address} URL
    </body>
</html>",
                Status = Status.OK
            };
        }

        public bool ShouldProcess(Request request)
        {
            return true;
        }
    }
}
