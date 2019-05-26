﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
