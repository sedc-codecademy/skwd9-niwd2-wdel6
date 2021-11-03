using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    internal interface IResponse<T> : IResponse
    {
        public T Message { get; set; }
    }

    public interface IResponse
    {
        public Status Status { get; set; }
    }
}
