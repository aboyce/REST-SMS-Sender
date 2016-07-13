using System.Web.Http;

namespace SMSSender.REST
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
                defaults: new {controller = "test", action = "welcome", id = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "ParameterisedApi",
                routeTemplate: "api/{controller}/{action}/{parameter1}/{parameter2}/{parameter3}",
                defaults: new {controller = "test", action = "welcome", parameter1 = RouteParameter.Optional, parameter2 = RouteParameter.Optional, parameter3 = RouteParameter.Optional });
        }
    }
}
