using ProductService.Models;
//using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
namespace ProductService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder(); //Creates an Entity Data Model (EDM) for the OData endpoint.
            builder.EntitySet<Product>("Products");//the name of the controller must match the name of the entity set. 
            // New code:
            builder.EntitySet<Supplier>("Suppliers");

            config.Routes.MapODataRoute("Odata", "odata", builder.GetEdmModel());//Adds a route for the endpoint.

        }
    }
}
