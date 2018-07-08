using Learning.C_Sharp;
using Learning.DesignPatterns.ChainOfResponsibility;
using Learning.DesignPatterns.ChainOfResponsibility.Contract;
using Learning.DesignPatterns.ChainOfResponsibility.Core;
using Learning.Dot_Net.DesignPatterns;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
   public class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();

            BindDependencies(kernel);

            ////1. MemoryManagement
                //// new MemoryManagement().Start();

            ////2.DesignPattern
            IDesignPattern designPattern = kernel.Get<IDesignPattern>();

            designPattern.RunChainOfResponsibility();
        }

        private  static void BindDependencies(IKernel kernel)
        {
           ////1.Design Pattern Services
           kernel.Bind<IDesignPattern>().To<DesignPattern>();

           ////2. Approver CHAIN OF RESPONSIBILITIES
           kernel.Bind<IApprover>().To<Director>() .InSingletonScope();
           kernel.Bind<IApprover>().To<VicePresident>().WhenInjectedInto <Director>();
           kernel.Bind<IApprover>().To<President>().WhenInjectedInto<VicePresident>().WithConstructorArgument<IApprover>(null);
        }
    }
}
