using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class CreateTransactionCommand : TransactionCommand
    {
        public Account Account { get; set; }
    }
}
