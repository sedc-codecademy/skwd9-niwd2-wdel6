using Sedc.Server.Core;
using Sedc.Server.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sedc_server_try_two
{
    class Result
    {
        public int First { get; set; }
        public int Second { get; set; }
        public string Operation { get; set; }
        public int Value { get; set; }
    }

    class CalculatorApiController : IApiController
    {
        public string Name { get => "Calculator Api Controller"; }

        public object Execute(IEnumerable<string> path, IDictionary<string, string> parameters, string method, ILogger logger)
        {
            logger.Debug($"Path: {string.Join(" / ", path)}");
            logger.Debug($"Params: {string.Join(" / ", parameters.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
            logger.Debug($"Method: {method}");

            path = path.Skip(1);

            var operation = path.First().ToLowerInvariant();
            var first = int.Parse(path.Skip(1).First());
            var second = int.Parse(path.Skip(2).First());

            var value = int.MinValue;
            if (operation == "add")
            {
                value = Add(first, second);
            } 
            else if (operation == "sub")
            {
                value = Subtract(first, second);
            }
            else if (operation == "mul")
            {
                value = Multiply(first, second);
            }
            else if (operation == "div")
            {
                value = Divide(first, second);
            }
            else
            {
                value = int.MinValue;
            }

            return new Result
            {
                First = first,
                Second = second,
                Operation = operation,
                Value = value
            };
        }

        public int Add(int first, int second)
        {
            return first + second;
        }
        public int Subtract(int first, int second)
        {
            return first - second;
        }
        public int Multiply(int first, int second)
        {
            return first * second;
        }
        public int Divide(int first, int second)
        {
            return first / second;
        }
    }
}
