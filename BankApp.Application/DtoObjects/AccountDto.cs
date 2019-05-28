using BankApp.Application.Exceptions;
using System;
using System.Collections.Generic;

namespace BankApp.Application.DtoObjects
{
    public class AccountDto
    {
        private decimal _Balance;

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public virtual List<TransactionDto> Transactions { get; set; }
    }
}