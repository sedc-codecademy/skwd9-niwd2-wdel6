using System;

namespace sedc_server_try_one
{
    internal class ResponseGenerator
    {
        public ResponseGenerator()
        {
        }

        internal Response GenerateResponse(Request request)
        {
            return new Response { Message = "Hi, I'm a SEDC Server, nice to meet you" };
        }
    }
}