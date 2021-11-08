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
        public string DefaultDocument { get; private set; }

        public FileRequestProcessor(string basePath, string defaultDocument = "index.html")
        {
            if (!Directory.Exists(basePath)) 
            {
                throw new ApplicationException($"Folder {basePath} does not exist. Please create it before starting the server");
            }
            BasePath = basePath;
            DefaultDocument = defaultDocument;
        }
        public IResponse Process(Request request, ILogger logger)
        {
            var fullPath = Path.Combine(request.Address.Path.ToArray());

            string filename = Path.Combine(BasePath, fullPath);

            // todo: check whether filename is actually inside the base path
            // In order to prevent Directory Traversal

            if (Directory.Exists(filename))
            {
                filename = Path.Combine(filename, DefaultDocument);
            }

            if (!File.Exists(filename))
            {
                logger.Error($"User tried to access non-existant file {filename}, returning Not Found");
                return new TextResponse
                {
                    Status = Status.NotFound
                };
            }

            var extension = Path.GetExtension(filename);

            try 
            {
                if (textExtensions.Contains(extension))
                {
                    logger.Info($"User tried to access text file {filename}, returning text response");
                    var output = File.ReadAllText(filename);
                    return new TextResponse
                    {
                        Message = output
                    };
                } 
                else
                {
                    logger.Info($"User tried to access binary file {filename}, returning binary response");
                    var output = File.ReadAllBytes(filename);
                    return new BinaryResponse
                    {
                        Message = output
                    };
                }
            }
            catch (Exception ex)
            {
                string message = $"Error occured when accessing file {filename}, {ex.Message}";
                throw new SedcServerException(message, ex);
            }
        }

        public bool ShouldProcess(Request request)
        {
            return true;
        }

        public string Describe() => $"FileRequestProcessor: Serving files from folder '{BasePath}'";
    }
}
