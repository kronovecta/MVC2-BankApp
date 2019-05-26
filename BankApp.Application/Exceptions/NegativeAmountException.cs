using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Exceptions
{
    class NegativeAmountException : Exception
    {
        private static readonly string DefaultMessage = "Negative amounts are not allowed";

        public NegativeAmountException()
        {

        }

        public NegativeAmountException(decimal amount) : base(DefaultMessage)
        {

        }
    }
}
