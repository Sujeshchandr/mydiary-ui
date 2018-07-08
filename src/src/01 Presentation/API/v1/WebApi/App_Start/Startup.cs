using Hangfire;
using Microsoft.Owin;
using MyDiary.API.App_Start;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(MyDiary.API.Startup))]

namespace MyDiary.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            SwaggerConfig.Register(config);

            //// Install-Package Microsoft.AspNet.WebApi.Owin
            //// Install-Package Ninject
            //// If you are using web api version 5.0.0.0 you also need to download the Ninject Resolver class 
            //// from the https://github.com/WebApiContrib/WebApiContrib.IoC.Ninject/blob/master/src/WebApiContrib.IoC.Ninject/NinjectResolver.cs to avoid compatability issues.
            config.DependencyResolver = new APINinjectDependencyResolver(new Ninject.Web.Common.Bootstrapper().Kernel);

            WebApiConfig.Register(config); // registering routes
            app.UseWebApi(config);

            Hangfire.GlobalConfiguration.Configuration
                    .UseSqlServerStorage("HangfireConnection")
                    .UseNinjectActivator(new Ninject.Web.Common.Bootstrapper().Kernel);
             
            //// app.UseCookieAuthentication(); // Authentication - first

            app.UseHangfireDashboard("/hangfire", new DashboardOptions { AppPath = "http://localhost:1034" }); // Hangfire - last

            app.UseHangfireServer(new BackgroundJobServerOptions() { WorkerCount = 1, Queues = new[] { "Priority", "Default" } });

            app.UseHangfireServer(new BackgroundJobServerOptions() { WorkerCount = 1, Queues = new[] { "File_Submit_Priority", "File_Submit_Default" } });
            
        }
    }
}