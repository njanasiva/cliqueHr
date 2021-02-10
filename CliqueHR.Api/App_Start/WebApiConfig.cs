using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
<<<<<<< HEAD
=======
using System.Web.Http.Cors;
>>>>>>> change

namespace CliqueHR.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
<<<<<<< HEAD
=======
            var cors = new EnableCorsAttribute("*", "*", "*"); // origins, headers, methods
            config.EnableCors(cors);
>>>>>>> change
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
<<<<<<< HEAD
        
    }
=======

        }
>>>>>>> change
    }

}
