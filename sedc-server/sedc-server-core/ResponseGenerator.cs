using System;

namespace Sedc.Server.Core
{
    internal class ResponseGenerator
    {

        private readonly IRequestProcessor processor;
        private readonly ILogger logger;
        public ResponseGenerator(IRequestProcessor processor, ILogger logger)
        {
            this.processor = processor;
            this.logger = logger;
        }

        internal IResponse GenerateResponse(Request request)
        {
            if (!ValidateRequest(request))
            {
                throw new ApplicationException("Validation failed");
                //return new Response { Message = "Invalid response"}
            }
            logger.Debug($"Running {processor.Describe()}");
            try {
                var response = processor.Process(request, logger);
                return response;
            }
            catch (SedcServerException ssex) {
                logger.Error(ssex.Message);
                return new TextResponse {
                    Message = Status.GenericErrorMessage,
                    Status  = Status.ServerError
                };
            }
            catch (Exception ex) {
                logger.Critical(ex.Message);
                throw;
            }
        }

        private bool ValidateRequest(Request request)
        {
            return true;
        }
    }
}
