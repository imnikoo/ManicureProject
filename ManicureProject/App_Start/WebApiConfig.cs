using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManicureProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           // Web API configuration and services
            var corsAttrs = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttrs);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
