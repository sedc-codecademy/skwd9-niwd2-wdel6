using Sedc.Server.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Helpers.ResponseHelpers
{
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
