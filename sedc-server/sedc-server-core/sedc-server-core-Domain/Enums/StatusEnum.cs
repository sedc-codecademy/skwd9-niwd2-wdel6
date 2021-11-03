using System.Collections.Generic;
using System.ComponentModel;

namespace Sedc.Server.Core.sedc_server_core_Domain.Enums
{
    public enum StatusEnum
    {
        OK = 200,
        ServerError = 500,
        NotFound = 404,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        SemanticError = 422
    }

    public static class StatusText
    {
        private static Dictionary<StatusEnum, string> textStatuses = new();

        static StatusText()
        {
            textStatuses.Add(StatusEnum.OK, "OK");
            textStatuses.Add(StatusEnum.ServerError, "Internal Server Error");
            textStatuses.Add(StatusEnum.NotFound, "Not Found");
        }

        static public string GetText(StatusEnum status)
        {
            if (!textStatuses.TryGetValue(status, out string text))
            {
                return status.ToString();
            }
            return text;
        }
    }
}