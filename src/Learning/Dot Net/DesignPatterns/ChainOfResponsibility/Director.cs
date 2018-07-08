using Learning.DesignPatterns.ChainOfResponsibility.Contract;
using Learning.DesignPatterns.ChainOfResponsibility.Core;
using Learning.Dot_Net.DesignPatterns.ChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DesignPatterns.ChainOfResponsibility
{
    public sealed class Director : Approver<Purchase>
    {
        protected IApprover _next;

        public Director(IApprover next)
            : base(next)
        {
            _next = next;
        }

        public IApprover Next
        {
            get
            {
                return _next;
            }
        }

        protected override bool Approve(Purchase purchase)
        {
            Console.WriteLine("{0} approved request# {1}", this.GetType().Name, purchase.Number);
            return true;
        }

        protected override bool CanHandle(Purchase purchase)
        {
            return (purchase.Amount < 10000);
        }
    }
}
