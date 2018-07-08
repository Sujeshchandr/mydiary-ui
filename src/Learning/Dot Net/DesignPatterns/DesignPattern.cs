using Learning.DesignPatterns.ChainOfResponsibility;
using Learning.DesignPatterns.ChainOfResponsibility.Contract;
using Learning.DesignPatterns.ChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Dot_Net.DesignPatterns
{
    public class DesignPattern : IDesignPattern
    {
        private readonly IApprover _approver;

        public DesignPattern(IApprover approver)
        {
            _approver = approver;
        }

        public  void RunChainOfResponsibility()
        {
            _approver.Approve(new Purchase(number: 2034, amount: 350.00, purpose: "Assets"));

            _approver.Approve(new Purchase(number: 2035, amount: 32590.10, purpose: "Project X"));

            _approver.Approve(new Purchase(number: 2036, amount: 122100.00, purpose: "Project Y"));

            // Wait for user
            Console.ReadKey();
        }
    }
}
