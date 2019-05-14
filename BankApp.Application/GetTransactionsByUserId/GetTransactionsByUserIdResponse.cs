using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries.GetTransactionsByUserId
{
    class GetTransactionsByUserIdResponse
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}