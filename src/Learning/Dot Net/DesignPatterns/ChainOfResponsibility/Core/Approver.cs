using Learning.DesignPatterns.ChainOfResponsibility.Contract;
using Learning.Dot_Net.DesignPatterns.ChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DesignPatterns.ChainOfResponsibility.Core
{
    public abstract class Approver<T> : IApprover where T : Activity
    {
        private IApprover _next;

        public Approver(IApprover next)
        {
            _next = next;
        }

        protected IApprover Next
        {
            get {
                return _next;
            }
        }

        public bool Approve(Activity activity)
        {
            if (!this.CanHandle((T)activity))
            {
                if (Next == null)
                {
                    Console.WriteLine("Request requires an executive meeting!");
                    return false;
                }
                else
                {
                    return Next.Approve((T)activity);
                }
            }

           return  this.Approve((T)activity);
        }

        protected abstract bool Approve(T activity);

        protected abstract bool CanHandle(T activity);
    }
}
