using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Ninject.Web.Common;
using Ninject.Extensions.Wcf;
using MyDiary.WCF.App_Start;
using Ninject;
using System.IO;

namespace MyDiary.WCF
{
    public class Global : Ninject.Web.Common.NinjectHttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
           // string sessionId = Session.SessionID;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
             //for fixing this issue "Session state has created a session id, but cannot save it because the response was already flushed by the application."
            string sessionId = Session.SessionID;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {

        }

     protected   void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();
        }
    
        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new WCFNinjectModule());
        }
    }
}