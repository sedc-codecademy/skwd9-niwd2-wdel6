using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public static class JsonSerializer
    {
        public static string AsString(object value)
        {
            if (value is int intValue)
            {
                return intValue.ToString();
            }
            if (value is bool boolValue)
            {
                return $"{boolValue.ToString().ToLowerInvariant()}";
            }
            if (value is string strValue)
            {
                strValue = strValue.Replace("\"", "\\\"");
                return $"\"{strValue}\"";
            }
            if (value is IDictionary dictionary)
            {
                var itemValues = new List<string>();
                foreach (DictionaryEntry item in dictionary)
                {
                    var keyString = AsString(item.Key);
                    var valueString = AsString(item.Value);
                    itemValues.Add($"{keyString}:{valueString}");
                }
                return $"{{{string.Join(",", itemValues)}}}";
            }
            if (value is IEnumerable enumerable)
            {
                //var enumValue = string.Join(",",enumerable.Cast<object>().Select(item => AsString(item)));
                //return $"[{enumValue}]";
                var itemValues = new List<string>();
                foreach (var item in enumerable)
                {
                    itemValues.Add(AsString(item));
                }
                return $"[{string.Join(",", itemValues)}]";
            }

            // generic c# object
            var type = value.GetType();
            var properties = type.GetProperties();
            var propValues = new List<string>();
            foreach (var pinfo in properties)
            {
                var pvalue = pinfo.GetGetMethod().Invoke(value, null);
                propValues.Add($"\"{pinfo.Name}\":{AsString(pvalue)}");
            }

            return $"{{{string.Join(",", propValues)}}}";
        }
    }
}
