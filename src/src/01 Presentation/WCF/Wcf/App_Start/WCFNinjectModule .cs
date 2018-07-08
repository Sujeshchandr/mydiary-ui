
using Ninject.Modules;
using NLog;

namespace MyDiary.WCF.App_Start
{
    public class WCFNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Injects the constructors of all DI-ed objects
           
            //Service;         
            Bind<ILogger>().ToMethod(context => LogManager.GetLogger("default")).InSingletonScope();
            Bind<MyDiary.Application.Services.Abstract.Images.IImageService>().To<MyDiary.Application.Services.Images.ImageService>();
            //.WhenInjectedInto();
                 //.WithConstructorArgument("imageTestDomain", x => x .Kernel. Get<MyDiary.Domain.Domains.TestImage>()); 

            //Domain
            Bind<MyDiary.Domain.Abstract.Domains.IImage>().To<MyDiary.Domain.Domains.Image>();
            //    .Named("image");  
            //Bind<MyDiary.Domain.Abstract.Domains.IImage>().To<MyDiary.Domain.Domains.TestImage>()
            //    .Named("testImage");           

            //Repositories          
            Bind<MyDiary.Domain.Abstract.Repositories.SQL.IImageRepository>().To<MYDiary.SQLProvider.Images.Managers.ImageManager>();
        }
    }
}
