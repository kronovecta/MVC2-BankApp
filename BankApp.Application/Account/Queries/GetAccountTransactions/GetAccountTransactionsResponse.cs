using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountTransactionsResponse
    {
        public AccountDto Account { get; set; }
        public IList<TransactionDto> MyProperty { get; set; }
    }
}
