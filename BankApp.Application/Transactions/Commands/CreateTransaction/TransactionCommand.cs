using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public abstract class TransactionCommand
    {
        public decimal Amount { get; set; }
        public string Operation { get; set; }
    }
}
