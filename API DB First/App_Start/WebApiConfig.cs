using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_DB_First
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //the json formatters are turned off. If we want the values in json format then automatically it shows the data
            //in XML format, because json format has been turned off.
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            
            //From different domain request can come. By default we this domain will not accept any request. Therefore
            //access must be given in order to pass data. We have to Install Microsoft.AspNet.WebApis.Cors from
            //Nuget Package Manager.
            //The following line is the an attribute of type EnableCorsAttribute. Here we are passing 3 arguments
            //The first argument defines which domains can send request to this domain. We can allow multiple domains. 
            //So here we allowed facebook and twitter for first argument 
            //The Second argument is the header. We can define multiple header.
            //And the third argument method. It defines only which methods are allowed for request.
            //EnableCorsAttribute cors = new EnableCorsAttribute("www.facebook.com,www.twitter.com", "Authorization,Accept", "GET,PUT,POST");
            
            //However if we want to allow "everything" the the following line is applicable
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);//The CORS is now enabled
        }
    }
}
