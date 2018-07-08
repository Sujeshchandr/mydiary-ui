using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyDiary.UI.ControllerHelpers;

namespace MyDiary.UI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AuthorizeLoginUser : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpSessionStateBase Session = filterContext.HttpContext.Session;

            int sessionUserId = Session.UserId();

            if (sessionUserId != -1 && sessionUserId != 0)
            {
                filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary{{ "controller", "Bootstrap" },
                                                {"action","Index"}
                                           });

            }

            //int sessionLoginId = Session.LoginId();

            //if (sessionLoginId != -1  && sessionLoginId != 0)
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //                                new RouteValueDictionary{{ "controller", "Bootstrap" },
            //                                    {"action","Index"}
            //                               });

            //}



        }

    }
}