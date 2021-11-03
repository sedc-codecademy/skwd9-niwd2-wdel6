namespace Sedc.Server.Core.sedc_server_core_App
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Server server = new();
            server.Start();

        }
    }
}
