using Sedc.MicroService.Models;

using Sedc.Server.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ms_authors_service
{
    internal class AuthorsApiController : IApiController
    {
        public string Name { get => nameof(AuthorsApiController); }

        public object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger)
        {
            // /api/authors

            logger.Debug($"Path: {string.Join(" / ", path)}");
            logger.Debug($"Params: {string.Join(" / ", parameters.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
            logger.Debug($"Method: {method}");

            path = path.Skip(1);

            var action = path.FirstOrDefault();

            if (string.IsNullOrEmpty(action)) {
                var authors = GetAuthors().Result;
                return new JsonResponse<IEnumerable<Author>> {
                    Message = authors
                };
            }

            // default
            action = action.ToLowerInvariant();
            return new
            {
                message = "Not Implemented"
            };
        }

        private async Task<IEnumerable<Author>> GetAuthors()
        {
            HttpClient hc = new HttpClient();
            var data = await hc.GetStringAsync("http://localhost:8081/mssql/table/authors/data");
            var output = System.Text.Json.JsonSerializer.Deserialize<List<object[]>>(data);

            var authors = output.Select(author => new Author
            {
                ID = int.Parse(author[0].ToString()),
                Name = author[1].ToString()
            });

            return authors;
        }
    }
}
