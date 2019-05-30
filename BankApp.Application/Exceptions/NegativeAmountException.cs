using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Exceptions
{
    public class NegativeAmountException : Exception
    {
        private static readonly string DefaultMessage = "Negative amounts are not allowed";

        public NegativeAmountException() : base(DefaultMessage)
        {

        }
    }
}
