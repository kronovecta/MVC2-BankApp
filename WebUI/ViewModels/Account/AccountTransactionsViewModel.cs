using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class AccountTransactionsViewModel
    {
        public AccountDto Account { get; set; }
        public IList<TransactionDto> Transactions { get; set; }

        public AccountTransactionsViewModel()
        {
            Transactions = new List<TransactionDto>();
        }
    }
}
