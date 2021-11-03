namespace Sedc.Server.Core.sedc_server_core_Domain.Models
{
    internal class Response
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public Response()
        {
            Status = Status.ServerError;
        }
    }
}