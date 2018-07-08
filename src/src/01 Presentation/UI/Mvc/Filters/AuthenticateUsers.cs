using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyDiary.UI.ControllerHelpers;
using MyDiary.UI.ViewModels;


namespace MyDiary.UI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AuthenticateUser  : AuthorizeAttribute 
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
              //HttpSessionStateBase Session = filterContext.HttpContext.Session;
              string userId = CookieHelper.GetDecryptedDiaryCookieValue(); //Authenticate user cookie
              if (string.IsNullOrEmpty(userId))
              {
                  if (filterContext.HttpContext.Request.IsAjaxRequest())
                  {
                      filterContext.RequestContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                      filterContext.RequestContext.HttpContext.Response.StatusDescription = "Un Authorized";
                      filterContext.Result = new JsonResult(){
                         
                          JsonRequestBehavior =System.Web.Mvc.JsonRequestBehavior.AllowGet,
                          Data = new CustomError(){

                              Code ="401",
                              Message ="Un Authorized"
                          }
                      };
                  }  
                  else
                  {
                       filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "People" }, { "action", "Index" } });
                  }

              }
        }
    }
}