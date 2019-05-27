using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Domain.Entities;

namespace BankApp.Application.Commands
{
    public class CreateTransferTransactionCommand : TransactionCommand
    {
        public Account Sender { get; set; }
        public Account Reciever { get; set; }
    }
}
