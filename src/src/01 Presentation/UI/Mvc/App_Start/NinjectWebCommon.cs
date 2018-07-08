[assembly: WebActivator.PreApplicationStartMethod(typeof(MyDiary.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MyDiary.UI.App_Start.NinjectWebCommon), "Stop")]

namespace MyDiary.UI.App_Start
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
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
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
            kernel.Bind<MyDiary.Application.Services.Abstract.Incomes.IIncomeService>().To<MyDiary.Application.Services.Incomes.IncomeService>().InSingletonScope();
            kernel.Bind<MyDiary.Application.Services.Abstract.IncomeTypes.IIncomeTypeService>().To<MyDiary.Application.Services.IncomeTypes.IncomeTypeService>().InSingletonScope();
            kernel.Bind<MyDiary.Application.Services.Abstract.Expenses.IExpenseService>().To<MyDiary.Application.Services.Expenses.ExpenseService>().InSingletonScope();
                //.WithConstructorArgument("expenseTestDomain", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
                 //.WithPropertyValue("TestInject", x => x.Kernel.Get<MyDiary.Domain.Domains.TestDomain>());
            kernel.Bind<MyDiary.Application.Services.Abstract.People.IPeopleService>().To<MyDiary.Application.Services.People.PeopleService>().InSingletonScope();
            kernel.Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>().Named("image");
            kernel.Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>().InSingletonScope();

            #endregion

            #region DOMAINS
         
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IIncome>().To<MyDiary.Domain.Domains.Income>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IIncomeType>().To<MyDiary.Domain.Domains.IncomeType>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IExpenseType>().To<MyDiary.Domain.Domains.ExpenseType>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IExpense>().To<MyDiary.Domain.Domains.Expense>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IPeople>().To<MyDiary.Domain.Domains.People>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IRole>().To<MyDiary.Domain.Domains.Role>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IImage>().To<MyDiary.Domain.Domains.Image>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IUserLogin>().To<MyDiary.Domain.Domains.UserLogin>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Domains.IOpenLogin>().To<MyDiary.Domain.Domains.OpenLogin>().InSingletonScope();
            #endregion

            #region  REPOSITORIES

            #region SQL REPOSITORIES

            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IIncomeRepository>().To<MYDiary.SQLProvider.Incomes.Managers.IncomeManager>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IIncomeTypeRepository>().To<MYDiary.SQLProvider.IncomeTypes.Managers.IncomeTypeManager>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IExpenseRepository>().To<MYDiary.SQLProvider.Expense.Managers.ExpenseManager>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IPeopleRepository>().To<MYDiary.SQLProvider.People.Managers.PeopleManager>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.SQL.IImageRepository>().To<MYDiary.SQLProvider.Images.Managers.ImageManager>().InSingletonScope();

                #endregion

            #region MONGO REPOSITORIES

            kernel.Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IIncomeMongoRepository>().To<MyDiary.MongoProvider.Managers.Incomes.IncomeMongoManager>().InSingletonScope();
            kernel.Bind<MyDiary.Domain.Abstract.Repositories.Mongo.IExpenseMongoRepository>().To<MyDiary.MongoProvider.Managers.Expenses.ExpenseMongoManager>().InSingletonScope();
            
            #endregion

            #endregion
        }        
    }
}
