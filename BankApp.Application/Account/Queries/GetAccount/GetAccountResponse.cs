using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries.GetAccount
{
    class GetAccountResponse
    {
        public class AccountDto
        {
            public string Frequency { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }

        public List<AccountDto> Accounts { get; set; }
    }
}
