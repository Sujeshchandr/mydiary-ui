using Learning.DesignPatterns.ChainOfResponsibility.Core;
using Learning.Dot_Net.DesignPatterns.ChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DesignPatterns.ChainOfResponsibility.Contract
{
    public interface IApprover
    {
        bool Approve(Activity activity);
    }
}
