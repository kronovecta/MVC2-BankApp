using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class DepositHandler
    {
        private readonly BankContext _context;

        public DepositHandler()
        {
            _context = new BankContext();
        }
        public async Task Handler(DepositCommand command)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
            account.Deposit(command.Amount);

            var transaction = new CreateTransactionCommand()
            {
                Operation = "Deposit in cash",
                Account = account,
                Amount = command.Amount
            };

            var handler = new CreateTransactionHandler(transaction);

            await _context.SaveChangesAsync();
        }
    }
}
