using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        private static readonly string DefaultMessage = "Account balance is insufficient for the transaction";

        public InsufficientFundsException() : base(DefaultMessage)
        {

        }
    }
}
