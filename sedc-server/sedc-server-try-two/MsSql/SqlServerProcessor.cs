using Sedc.Server.Core;

namespace Sedc.Server.TryTwo.MsSql;
internal class SqlServerProcessor : IRequestProcessor
{
    public string Prefix { get; init; }

    public SqlServerProcessor(string prefix)
    {
        Prefix = prefix;
    }

    public IResponse Process(Request request, ILogger logger)
    {
        var path = request.Address.Path.Skip(1);
        var actionMethod = SqlServerFactory.GetAction(path);
        var response = actionMethod();
        return response;
    }

    // public IResponse Process(Request request, ILogger logger) => SqlServerFactory.GetAction(request.Address.Path.Skip(1))();

    public bool ShouldProcess(Request request)
    {
        if (!request.Address.Path.Any())
        {
            return false;
        }
        var prefix = request.Address.Path.First();
        return prefix == Prefix;
    }
}