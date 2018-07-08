using System.Web.Mvc;
using System.Web.Routing;

namespace MyDiary.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //// routes.MapRoute(
            ////        name: "testDefault",
            ////        url: "Test",
            ////        defaults: new { controller = "Test", action = "Index", id = UrlParameter.Optional }
            //// );
            //// routes.MapRoute(
            ////       name: "JqueryDataTable",
            ////       url: "JqueryDataTable",
            ////       defaults: new { controller = "Test", action = "JqueryDataTable", id = UrlParameter.Optional }
            ////);
            //// routes.MapRoute(
            ////       name: "getDataTableJson",
            ////       url: "getDataTableJson",
            ////       defaults: new { controller = "Test", action = "GetJsonForDataTable", id = UrlParameter.Optional }
            ////);
            ////routes.MapRoute(
            ////       name: "homeDefault",
            ////       url: "Home",
            ////       defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ////);

            ////  routes.MapRoute(
            ////         name: "incomeDefault",
            ////         url: "Income",
            ////         defaults: new { controller = "Income", action = "Index", id = UrlParameter.Optional }
            ////  );

            ////  routes.MapRoute(
            ////         name: "incomediary",
            ////         url: "Income/{action}",
            ////         defaults: new { controller = "Income", action = "{action}", id = UrlParameter.Optional }
            //// );
            ////  routes.MapRoute(
            ////         name: "expenseDefault",
            ////         url: "Expense",
            ////         defaults: new { controller = "Expense", action = "Index", id = UrlParameter.Optional }
            //// );
            ////  routes.MapRoute(
            ////         name: "expensediary",
            ////         url: "Expense/{action}",
            ////         defaults: new { controller = "Expense", action = "{action}", id = UrlParameter.Optional }
            ////);
            ////routes.MapRoute(
            ////       name: "bootstrap",
            ////       url: "Bootstrap/{action}",
            ////       defaults: new { controller = "Bootstrap", action = "{action}" }
            ////);

            ////routes.MapRoute(
            ////       name: "Default",
            ////       url: "{controller}/{action}/{id}",
            ////       defaults: new { controller = "{controller}", action = "Index", id = UrlParameter.Optional }
            ////);

        }
    }
}