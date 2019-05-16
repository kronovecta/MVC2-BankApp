using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountResponse
    {
        public List<AccountDto> Accounts { get; set; }
    }
}
