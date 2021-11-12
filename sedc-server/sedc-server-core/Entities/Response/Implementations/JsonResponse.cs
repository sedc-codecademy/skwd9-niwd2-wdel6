using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Response.Implementations
{
    public class JsonResponse<T> : JsonResponse, IResponse<T>
    {
        public T Message { get; set; }

        public override string GetMessagePayload()
        {
            return JsonSerializer.AsString(Message);
        }
    }

    public abstract class JsonResponse : IResponse
    {
        public Status Status { get; set; }

        public abstract string GetMessagePayload();

        public JsonResponse()
        {
            Status = Status.OK;
        }
    }
}
