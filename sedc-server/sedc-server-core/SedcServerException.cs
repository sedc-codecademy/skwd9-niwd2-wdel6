using System;
using System.Runtime.Serialization;

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


