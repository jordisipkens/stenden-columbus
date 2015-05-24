using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Linq;
using WebserviceColumbus.IO;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Travel;
using System.Data.Entity.Validation;
using System;

namespace WebserviceColumbus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}