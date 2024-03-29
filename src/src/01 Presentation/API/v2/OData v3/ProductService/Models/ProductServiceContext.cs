﻿using System.Data.Entity;

namespace ProductService.Models
{
    public class ProductServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductService.Models.ProductServiceContext>());

        public ProductServiceContext() : base("name=ProductServiceContext")
        {
        }
        public System.Data.Entity.DbSet<ProductService.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<ProductService.Models.Supplier> Suppliers { get; set; }
        //public System.Data.Entity.DbSet<ProductService.Models.ProductRating> Ratings { get; set; }
    }
}
