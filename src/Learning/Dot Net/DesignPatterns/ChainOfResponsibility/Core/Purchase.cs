using Learning.Dot_Net.DesignPatterns.ChainOfResponsibility.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DesignPatterns.ChainOfResponsibility.Core
{
    public class Purchase : Activity
    {
        private int _number;
       
        private string _purpose;

        // Constructor
        public Purchase(int number, double amount, string purpose)
            : base("Purchase", amount)
        {
            this._number = number;
            this._purpose = purpose;
        }

        // Gets or sets purchase number
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        // Gets or sets purchase purpose
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }

    }
}
