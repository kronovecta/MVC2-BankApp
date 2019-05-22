using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountsByUserIdRequest
    {
        public int CustomerId { get; set; }
    }
}
