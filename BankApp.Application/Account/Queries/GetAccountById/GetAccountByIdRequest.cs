﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdRequest
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
}
