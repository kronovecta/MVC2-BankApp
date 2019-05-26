using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class TransferCommand
    {
        public int AccountId_Sender { get; set; }
        public int AccountId_Reciever { get; set; }
        public decimal Amount { get; set; }
    }
}
