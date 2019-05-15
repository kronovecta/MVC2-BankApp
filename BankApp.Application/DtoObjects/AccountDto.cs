using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.DtoObjects
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
