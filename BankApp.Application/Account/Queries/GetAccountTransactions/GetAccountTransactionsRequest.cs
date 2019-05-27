using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountTransactionsRequest
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
        public int Page { get; set; }
    }
}
