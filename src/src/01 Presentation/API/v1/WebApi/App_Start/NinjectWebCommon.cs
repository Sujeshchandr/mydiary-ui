[assembly: WebActivator.PreApplicationStartMethod(typeof(MyDiary.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MyDiary.API.App_Start.NinjectWebCommon), "Stop")]

namespace MyDiary.API.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using NLog;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {

            var kernel = new StandardKernel();
            
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            ////System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new APINinjectDependencyResolver(kernel); 
           
            RegisterServices(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            #region SERVICES

            kernel.Bind<ILogger>().ToMethod(context => LogManager.GetLogger("default")).InSingletonScope();
            kernel.Bind<MyDiary.Application.Services.Abstract.Incomes.IIncomeService>().To<MyDiary.Application.Services.Incomes.IncomeService>();
            kernel.Bind<MyDiary.Application.Services.Abstract.Expenses.IExpenseService>().To<MyDiary.Application.Services.Expenses.ExpenseService>();
            //.WithConstructorArgument("expenseTestDomain", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
            //.WithPropertyValue("TestInject", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
            kernel.Bind<MyDiary.Application.Services.Abstract.People.IPeopleService>().To<MyDiary.Application.Services.People.PeopleService>();
            kernel.Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>().Named("image");
            kernel.Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>(); 

            #endregion

            #region DOMAIN

            kernel.Bind<MyDiary.Domain.Abstract.Domains.IIncomeType>().To<MyDiary.Domain.Domains.IncomeType>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IIncome>().To<MyDiary.Domain.Domains.Income>().InRequestScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IExpenseType>().To<MyDiary.Domain.Domains.ExpenseType>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IExpense>().To<MyDiary.Domain.Domains.Expense>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IPeople>().To<MyDiary.Domain.Domains.People>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IRole>().To<MyDiary.Domain.Domains.Role>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IImage>().To<MyDiary.Domain.Domains.Image>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IUserLogin>().To<MyDiary.Domain.Domains.UserLogin>();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IOpenLogin>().To<MyDiary.Domain.Domains.OpenLogin>();
            
            #endregion

            #region SQL REPOSITORIES

            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IIncomeRepository>().To<MYDiary.SQLProvider.Incomes.Managers.IncomeManager>();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IExpenseRepository>().To<MYDiary.SQLProvider.Expense.Managers.ExpenseManager>();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IPeopleRepository>().To<MYDiary.SQLProvider.People.Managers.PeopleManager>();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IImageRepository>().To<MYDiary.SQLProvider.Images.Managers.ImageManager>(); 

            #endregion

            #region MONGO REPOSITORIES

            kernel.Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IIncomeMongoRepository>().To<MyDiary.MongoProvider.Managers.Incomes.IncomeMongoManager>();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IExpenseMongoRepository>().To<MyDiary.MongoProvider.Managers.Expenses.ExpenseMongoManager>();

            #endregion

            #region HANGFIRE

            kernel.Bind<Hangfire.IBackgroundJobClient>().To<Hangfire.BackgroundJobClient>();

            #endregion
        }        
    }
}
