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
    public class DepositHandler : IRequestHandler<DepositCommand>
    {
        private readonly IBankContext _context;

        public DepositHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DepositCommand command, CancellationToken cancellationToken)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
            account.Balance += command.Amount;

            var transaction = new Transaction()
            {
                AccountId = account.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Deposit in cash",
                Amount = command.Amount,
                Balance = account.Balance,
                Bank = "CT"
            };

            await _context.Transactions.AddAsync(transaction);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
