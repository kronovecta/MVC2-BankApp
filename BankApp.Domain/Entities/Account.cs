using BankApp.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace BankApp.Domain.Entities
{
    public partial class Account
    {
        private decimal _Balance;

        public Account()
        {
            Dispositions = new HashSet<Disposition>();
            Loans = new HashSet<Loan>();
            PermenentOrder = new HashSet<PermanentOrder>();
            Transactions = new HashSet<Transaction>();
        }

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get { return _Balance; } }

        public virtual ICollection<Disposition> Dispositions { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<PermanentOrder> PermenentOrder { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

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
            }
            else
            {
                throw new NegativeAmountException();
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _Balance += amount;
            }
            else
            {
                throw new NegativeAmountException();
            }
        }
    }
}
