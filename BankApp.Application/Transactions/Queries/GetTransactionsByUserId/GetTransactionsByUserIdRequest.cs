using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries.GetTransactionsByUserId
{
    class GetTransactionByUserIdRequest
    {
        public int AccountId { get; set; }
    }
}
