using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sedc.Server.Core
{
    public record UrlAddress
    {
        private readonly string[] paths = new string[0];
        public IEnumerable<string> Path {
            get {
                return new ReadOnlyCollection<string>(paths);
            }
        }

        private readonly Dictionary<string, string> parameters = new();
        public IDictionary<string, string> Params { 
            get {
                return new ReadOnlyDictionary<string, string>(parameters);
            }
        }

        public UrlAddress(string url)
        {
            var regex = new Regex(@"^([^?]*)(?:\?(.*))?$");
            var match = regex.Match(url);
            if (!match.Success)
            {
                throw new ApplicationException($"Invalid url: {url}");
            }

            if (match.Groups[1].Success)
            {
                // NOT PROPER URL DECODING!!
                paths = match.Groups[1].Value.Replace("%20", " ").Split('/', StringSplitOptions.RemoveEmptyEntries);
            }

            if (match.Groups[2].Success)
            {
                var query = match.Groups[2].Value;
                var queryParts = query.Split('&');
                foreach (var queryPart in queryParts)
                {
                    var parts = queryPart.Split('=');
                    if (parts.Length != 2)
                    {
                        throw new ApplicationException($"Invalid query part: {queryPart}");
                    }

                    parameters.Add(parts[0], parts[1]);
                }
            }
        }

        public static UrlAddress FromString(string value)
        {
            return new UrlAddress(value);
        }

        public override string ToString()
        {
            var path = string.Join('/', paths);
            var query = string.Join('&', parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            if (string.IsNullOrEmpty(query)) {
                return path;
            }
            return $"{path}?{query}";
        }
    }

}