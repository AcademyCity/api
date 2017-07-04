using Core.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //跨域配置
            //var allowOrigins = ConfigurationManager.AppSettings["cors_allowOrigins"];
            //var allowHeaders = ConfigurationManager.AppSettings["cors_allowHeaders"];
            //var allowMethods = ConfigurationManager.AppSettings["cors_allowMethods"];
            //var globalCors = new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods);
            //config.EnableCors(globalCors);
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
