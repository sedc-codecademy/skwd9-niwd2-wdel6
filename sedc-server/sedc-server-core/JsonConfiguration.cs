using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class JsonConfiguration
    {
        private readonly JsonElement data;
        public JsonConfiguration(string jsonFileName)
        {
            try
            {
                var authorsText = File.ReadAllText(jsonFileName);
                data = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(authorsText);
            }
            catch (Exception ex)
            {
                throw new SedcServerException($"Error reading {jsonFileName}", ex);
            }
        }


        public string GetConnectionString(string name)
        {
            return data.GetProperty("ConnectionStrings").GetProperty(name).GetString();
        }

    }
}
