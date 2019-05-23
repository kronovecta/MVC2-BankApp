using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdResponse
    {
        public AccountDto Account { get; set; }
        public int TotalTransactions { get; set; }
        public int TotalPages { get; set; }
        public IList<TransactionDto> Transactions { get; set; }

        public GetAccountByIdResponse()
        {
            Transactions = new List<TransactionDto>();
        }
    }
}
