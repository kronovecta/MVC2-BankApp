using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountsByUserIdResponse
    {
        public List<AccountDto> Accounts { get; set; }
    }
}
