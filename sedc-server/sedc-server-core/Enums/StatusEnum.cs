using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Enums
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
}
