using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Helpers.CustomExceptions
{
    class SedcServerException : ApplicationException
    {
        public SedcServerException(string message) : base(message)
        {
        }

        public SedcServerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SedcServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
