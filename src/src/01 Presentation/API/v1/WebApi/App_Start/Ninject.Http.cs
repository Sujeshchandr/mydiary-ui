using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Ninject;

//  A small Library to configure Ninject (A Dependency Injection Library) with a WebAPI Application. 
//  To configure, take the following steps.
// 
//  1. Install Packages Ninject and Ninject.Web.Common 
//  2. Remove NinjectWebCommon.cs in your App_Start Directory
//  3. Add this file to your project  (preferrably in the App_Start Directory)  
//  4. Add Your Bindings to the Load method of MainModule. 
//     You can add as many additional modules to keep things organized
//     simply add them to the Modules property of the NinjectModules class
//  5. Add the following Line to your Global.asax
//          NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);  
//  5b.To Automatically Register all NinjectModules in the Current Project, You should instead add
//          NinjectContainer.RegisterAssembly()
//  You are done. 

namespace MyDiary.API.App_Start
{
    /// <summary>
    /// Resolves Dependencies Using Ninject
    /// </summary>
    public class NinjectHttpResolver : IDependencyResolver, IDependencyScope
    {
        public IKernel Kernel { get; private set; }
        public NinjectHttpResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectHttpResolver(Assembly assembly)
        {
            Kernel = new StandardKernel();
            Kernel.Load(assembly);
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            //Do Nothing
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }


    // List and Describe Necessary HttpModules
    // This class is optional if you already Have NinjectMvc
    public class NinjectHttpModules
    {
        //Return Lists of Modules in the Application
        public static NinjectModule[] Modules
        {
            get
            {
                return new[] { new MainModule() };
            }
        }

        //Main Module For Application
        public class MainModule : NinjectModule
        {
            public override void Load()
            {
                //TODO: Bind to Concrete Types Here

               #region SERVICES

               Bind<MyDiary.Application.Services.Abstract.Incomes.IIncomeService>().To<MyDiary.Application.Services.Incomes.IncomeService>();
               Bind<MyDiary.Application.Services.Abstract.Expenses.IExpenseService>().To<MyDiary.Application.Services.Expenses.ExpenseService>();
               //.WithConstructorArgument("expenseTestDomain", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
               //.WithPropertyValue("TestInject", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
               Bind<MyDiary.Application.Services.Abstract.People.IPeopleService>().To<MyDiary.Application.Services.People.PeopleService>();
               Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>().Named("image");
               Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>();

               #endregion

               #region DOMAIN

               Bind<MyDiary.Domain.Abstract.Domains.IIncomeType>().To<MyDiary.Domain.Domains.IncomeType>();
               Bind<MyDiary.Domain.Abstract.Domains.IIncome>().To<MyDiary.Domain.Domains.Income>();
               Bind<MyDiary.Domain.Abstract.Domains.IExpenseType>().To<MyDiary.Domain.Domains.ExpenseType>();
               Bind<MyDiary.Domain.Abstract.Domains.IExpense>().To<MyDiary.Domain.Domains.Expense>();
               Bind<MyDiary.Domain.Abstract.Domains.IPeople>().To<MyDiary.Domain.Domains.People>();
               Bind<MyDiary.Domain.Abstract.Domains.IRole>().To<MyDiary.Domain.Domains.Role>();
               Bind<MyDiary.Domain.Abstract.Domains.IImage>().To<MyDiary.Domain.Domains.Image>();
               Bind<MyDiary.Domain.Abstract.Domains.IUserLogin>().To<MyDiary.Domain.Domains.UserLogin>();
               Bind<MyDiary.Domain.Abstract.Domains.IOpenLogin>().To<MyDiary.Domain.Domains.OpenLogin>();

               #endregion

               #region SQL REPOSITORIES

               Bind<MyDiary.Domain.Abstract.Repositories.SQL.IIncomeRepository>().To<MYDiary.SQLProvider.Incomes.Managers.IncomeManager>();
               Bind<MyDiary.Domain.Abstract.Repositories.SQL.IExpenseRepository>().To<MYDiary.SQLProvider.Expense.Managers.ExpenseManager>();
               Bind<MyDiary.Domain.Abstract.Repositories.SQL.IPeopleRepository>().To<MYDiary.SQLProvider.People.Managers.PeopleManager>();
               Bind<MyDiary.Domain.Abstract.Repositories.SQL.IImageRepository>().To<MYDiary.SQLProvider.Images.Managers.ImageManager>();

               #endregion

               #region MONGO REPOSITORIES

               Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IIncomeMongoRepository>().To<MyDiary.MongoProvider.Managers.Incomes.IncomeMongoManager>();
               Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IExpenseMongoRepository>().To<MyDiary.MongoProvider.Managers.Expenses.ExpenseMongoManager>();

               #endregion

               #region HANGFIRE

               Bind<Hangfire.IBackgroundJobClient>().To<Hangfire.BackgroundJobClient>();

               #endregion
            }
        }
    }


    /// <summary>
    /// Its job is to Register Ninject Modules and Resolve Dependencies
    /// </summary>
    public class NinjectHttpContainer
    {
        private static NinjectHttpResolver _resolver;

        //Register Ninject Modules
        public static void RegisterModules(NinjectModule[] modules)
        {
            _resolver = new NinjectHttpResolver(modules);
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        public static void RegisterAssembly()
        {
            _resolver = new NinjectHttpResolver(Assembly.GetExecutingAssembly());
            //This is where the actual hookup to the Web API Pipeline is done.
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        //Manually Resolve Dependencies
        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }
    }
}