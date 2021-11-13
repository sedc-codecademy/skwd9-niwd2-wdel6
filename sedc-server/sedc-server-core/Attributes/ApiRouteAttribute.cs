using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiRouteAttribute : Attribute
    {
        public string Route { get; set; }

        public ApiRouteAttribute() { }

        public ApiRouteAttribute(string route)
        {
            Route = route;
        }
    }
}
