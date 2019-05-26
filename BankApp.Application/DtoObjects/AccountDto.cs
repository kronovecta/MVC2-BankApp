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
        public decimal Balance { get { return _Balance; } set { _Balance = value; } } // Remove set, replace with deposit method

        public virtual List<TransactionDto> Transactions { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                if (amount < _Balance)
                {
                    _Balance -= amount;

                }
                else
                {
                    throw new InsufficientFundsException();
                }
            } else
            {
                throw new NegativeAmountException();
            }
        }

        public void Deposit(decimal amount)
        {
            if(amount > 0)
            {
                _Balance += amount;
            } else
            {
                throw new NegativeAmountException();
            }
        }
    }
}