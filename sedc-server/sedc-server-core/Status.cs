namespace Sedc.Server.Core
{
    public enum Status
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