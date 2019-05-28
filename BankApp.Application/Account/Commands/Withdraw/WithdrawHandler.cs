using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class WithdrawHandler
    {
        private readonly BankContext _context;

        public WithdrawHandler(BankContext context)
        {
            //_context = new BankContext();
            _context = context;
        }
        public async Task Handler(WithdrawCommand command)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId);
            account.Balance -= command.Amount;

            var transaction = new CreateTransactionCommand()
            {
                Operation = "Withdraw in cash",
                Account = account,
                Amount = command.Amount
            };

            //var handler = new CreateTransactionHandler(transaction);

            await _context.SaveChangesAsync();
        }   
    }
}
