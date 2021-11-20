using Sedc.MicroService.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_bff.Clients
{
    class AuthorsMessage
    {
        public IEnumerable<Author> Message { get; set; }
    }

    internal class AuthorServiceClient
    {
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            HttpClient hc = new HttpClient();
            var data = await hc.GetStringAsync("http://localhost:8082/api/authors");
            var authors = System.Text.Json.JsonSerializer.Deserialize<AuthorsMessage>(data);
            return authors.Message;
        }
    }
}
