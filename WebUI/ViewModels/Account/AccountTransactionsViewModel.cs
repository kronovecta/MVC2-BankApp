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
        public int AccountId { get; set; }
        public int PageNr { get; set; }
        public int NextPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalTransactions { get; set; }

        public IList<TransactionDto> Transactions { get; set; }

        public AccountTransactionsViewModel()
        {
            Transactions = new List<TransactionDto>();
        }
    }
}
