namespace Sedc.Server.Core
{
    public class TextResponse: IResponse<string>
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public TextResponse()
        {
            Status = Status.OK;
        }
    }
}