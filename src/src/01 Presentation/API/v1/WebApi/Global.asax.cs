using System;
using Hangfire;

namespace MyDiary.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly object _lockObject = new object();

        private BackgroundJobServer _backgroundJobServer;

        private BackgroundJobServer _backgroundFileSubmitJobServer;


        protected void Application_Start()
        {
            //// if using owin this register is throwing exception

            //WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            //NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();
        }

        protected void application_end(object sender, EventArgs e)
        {
            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                {
                    _backgroundJobServer.Dispose();
                }

                if (_backgroundFileSubmitJobServer != null)
                {
                    _backgroundFileSubmitJobServer.Dispose();
                } 
            }


        }

    }
}