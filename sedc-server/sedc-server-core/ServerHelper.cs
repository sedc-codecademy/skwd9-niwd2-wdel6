using System;
using System.Linq;

namespace Sedc.Server.Core
{
    internal class ServerHelper
    {
        internal static IRequestProcessor ConstructProcessor<T>(object[] parameters) where T : IRequestProcessor
        {
            var paramTypes = parameters.Select(p => p.GetType()).ToArray();
            var processor = typeof(T).GetConstructor(paramTypes).Invoke(parameters) as IRequestProcessor;
            return processor;
        }
    }
}