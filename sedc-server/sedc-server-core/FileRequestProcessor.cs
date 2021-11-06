using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class FileRequestProcessor : IRequestProcessor
    {
        private string[] textExtensions = new[] { ".html", ".txt" };

        public string BasePath { get; private set; }

        public FileRequestProcessor(string basePath)
        {
            if (!Directory.Exists(basePath)) 
            {
                throw new ApplicationException($"Folder {basePath} does not exist. Please create it before starting the server");
            }
            BasePath = basePath;
        }
        public IResponse Process(Request request, ILogger logger)
        {
            if (request.Address.Path.Count == 0)
            {
                logger.Warning($"The path is empty, returning Bad Request");
                return new TextResponse
                {
                    Status = Status.BadRequest
                };
            }
            string filename = Path.Combine(BasePath, request.Address.Path[0]);
            if (!File.Exists(filename))
            {
                logger.Error($"User tried to access non-existant file {filename}, returning Not Found");
                return new TextResponse
                {
                    Status = Status.NotFound
                };
            }

            var extension = Path.GetExtension(filename);

            if (textExtensions.Contains(extension))
            {
                logger.Info($"User tried to access text file {filename}, returning text response");
                var output = File.ReadAllText(filename);
                return new TextResponse
                {
                    Message = output
                };
            } else
            {
                logger.Info($"User tried to access binary file {filename}, returning binary response");
                var output = File.ReadAllBytes(filename);
                return new BinaryResponse
                {
                    Message = output
                };
            }

        }

        public string Describe() => $"FileRequestProcessor: Serving files from folder '{BasePath}'";
    }
}
