using BankApp.Application.Exceptions;
using BankApp.Common;
using BankApp.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class ApplyInterestHandler : IRequestHandler<ApplyInterestCommand>
    {
        private readonly IBankContext _context;
        private readonly IDateTime _dateTime;
        public ApplyInterestHandler(IBankContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(ApplyInterestCommand command, CancellationToken cancellationToken)
        {
            if (command.Amount > 0)
            {
                var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
                var dateDiff = Convert.ToDecimal((DateTime.Parse(_dateTime.Now.ToString()) - command.PreviousApplication).TotalDays);

                int frequency = 365;

                if (account.Frequency.ToLower() == "weekly")
                {
                    frequency = 52;
                }
                else if (account.Frequency.ToLower() == "monthly")
                {
                    frequency = 12;
                }

                var percent = command.Amount / 100;
                var interest = Convert.ToDouble(1 + (percent / frequency));

                var balance = Math.Abs(account.Balance * Convert.ToDecimal(Math.Pow(interest, frequency)));
                account.Balance = balance;
                await _context.SaveChangesAsync();

                var transaction = new CreateTransactionCommand { Account = account, Amount = balance, Operation = "Compound interest" };

                return Unit.Value;
            }
            else
            {
                throw new NegativeAmountException();
            }
        }
    }
}
