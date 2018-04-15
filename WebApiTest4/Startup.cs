using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Ninject;
using System.Reflection;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Services;
using WebApiTest4.Services.Impls;
using Ninject.Web.WebApi;

[assembly: OwinStartup(typeof(WebApiTest4.Startup))]

namespace WebApiTest4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            ConfigureWebApi(app);
        }

        private HttpConfiguration ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            SwaggerConfig.Register(config);

            //config.Filters.Add(new WebApiExceptionFilter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseNinjectMiddleware(CreateKernel);
            app.UseNinjectWebApi(config);

            return config;
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
