using BankApp.Application.Exceptions;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.Application.Commands
{
    public class ApplyInterestHandler
    {
        private readonly BankContext _context;
        public ApplyInterestHandler(BankContext context)
        {
            _context = context;
        }

        public void Handler(ApplyInterestCommand command)
        {
            if(command.Amount > 0)
            {
                var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
                var dateDiff = Convert.ToDecimal((DateTime.Now - command.PreviousApplication).TotalDays);

                int frequency = 365;

                if(account.Frequency.ToLower() == "weekly")
                {
                    frequency = 52;
                } else if(account.Frequency.ToLower() == "monthly")
                {
                    frequency = 12;
                }

                var percent = command.Amount / 100;
                var interest = Convert.ToDouble(1 + (percent / frequency));

                var balance = Math.Abs(account.Balance * Convert.ToDecimal(Math.Pow(interest, frequency)));
                _context.SaveChanges();

                var transaction = new CreateTransactionCommand { Account = account, Amount = balance, Operation = "Compound interest" };
                //var handler = new CreateTransactionHandler(transaction);
            } else
            {
                throw new NegativeAmountException();
            }   
        }
    }
}
