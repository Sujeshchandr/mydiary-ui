using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Dot_Net.DesignPatterns.ChainOfResponsibility.Core
{
    public abstract class Activity
    {
        public double Amount { get; set; }

        public string ActivityName { get; set; }

        public Activity() { }

        public Activity(string activityName, double amount)
        {
            if (activityName == null)
            {
                throw new ArgumentNullException("activityName");
            }

            ActivityName = activityName;
            Amount = amount;
        }
    }
}
