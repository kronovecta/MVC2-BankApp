using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class CreateTransactionHandler
    {
        private readonly BankContext _context;

        public CreateTransactionHandler(CreateTransactionCommand command, BankContext context)
        {
            _context = context;
            Handler(command);
        }

        public async Task Handler(CreateTransactionCommand command)
        {
            var transaction = new Transaction()
            {
                AccountId = command.Account.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Amount = command.Amount,
                Balance = command.Account.Balance + command.Amount,
                Bank = "CT"
            };

            if(command.Operation != null || command.Operation != "")
            {
                transaction.Operation = command.Operation;
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
