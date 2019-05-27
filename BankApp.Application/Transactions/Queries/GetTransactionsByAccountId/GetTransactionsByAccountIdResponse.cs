using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetTransactionsByAccountIdResponse
    {
        public int TotalTransactions { get; set; }
        public int TotalPages { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
