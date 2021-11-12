using Sedc.Server.Core.Entities;
using Sedc.Server.Core.Helpers.CustomExceptions;
using Sedc.Server.Core.Logging.Interfaces;
using Sedc.Server.Core.Response;
using Sedc.Server.Core.Response.Implementations;
using Sedc.Server.Core.Services.RequestService.Interfaces;
using System;
using System.Collections.Generic;

namespace Sedc.Server.Core
{
    internal class ResponseGenerator
    {

        private readonly IEnumerable<IRequestProcessor> processors;
        private readonly ILogger logger;
        public ResponseGenerator(IEnumerable<IRequestProcessor> processors, ILogger logger)
        {
            this.processors = processors;
            this.logger = logger;
        }

        internal IResponse GenerateResponse(Request request)
        {
            if (!ValidateRequest(request))
            {
                throw new ApplicationException("Validation failed");
                //return new Response { Message = "Invalid response"}
            }

            IRequestProcessor processor = null;
            foreach (var candidate in processors) {
                if (candidate.ShouldProcess(request)) {
                    processor = candidate;
                    break;
                }
            }

            logger.Debug($"Running {processor.Describe()}");
            try {
                var response = processor.Process(request, logger);
                return response;
            }
            catch (SedcServerException ssex) {
                logger.Error(ssex.Message);
                Exception ex = ssex;
                while(ex.InnerException != null) {
                    ex = ex.InnerException;
                    logger.Error(ex.Message);
                }

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
