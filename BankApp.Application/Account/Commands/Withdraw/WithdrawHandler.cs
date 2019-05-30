using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, WithdrawResponse>
    {
        private readonly BankContext _context;

        public WithdrawHandler(BankContext context)
        {
            _context = context;
        }

        public async Task<WithdrawResponse> Handle(WithdrawCommand command, CancellationToken cancellationToken)
        {
            if(command.Amount > 0)
            {
                var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
                if (account.Balance > command.Amount)
                {
                    account.Balance -= command.Amount;

                    var transaction = new Transaction()
                    {
                        AccountId = account.AccountId,
                        Date = DateTime.Now,
                        Type = "Debit",
                        Operation = "Withdraw in cash",
                        Amount = command.Amount,
                        Balance = account.Balance,
                        Bank = "CT"
                    };

                    await _context.Transactions.AddAsync(transaction);

                    await _context.SaveChangesAsync();

                    return new WithdrawResponse();
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
    }
}
