using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Rest.Web.Engineer.Filters;

namespace Rest.Web.Engineer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Konfiguracja i usługi składnika Web API
            config.Filters.Add(new ErrorFilter());

            // Trasy składnika Web API
            config.MapHttpAttributeRoutes();

            //Remove self-referencing LOOP
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;

            config.Routes.MapHttpRoute(
                name: "CartProducts",
                routeTemplate: "api/cart/{token}/product/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    controller = "CartProduct",
                    token = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "Carts",
                routeTemplate: "api/cart/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Cart" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{*sth}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}