using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class CreateTransferTransactionHandler
    {
        private readonly IBankContext _context;

        public CreateTransferTransactionHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task Handler(CreateTransferTransactionCommand command)
        {
            var transactionSender = new Transaction()
            {
                AccountId = command.Sender.AccountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = $"Transfer to {command.Reciever.AccountId}",
                Amount = command.Amount,
                Balance = command.Sender.Balance - command.Amount,
                Bank = "CT"
            };

            var transactionReciever = new Transaction()
            {
                AccountId = command.Reciever.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = $"Transfer from {command.Reciever.AccountId}",
                Amount = command.Amount,
                Balance = command.Reciever.Balance + command.Amount,
                Bank = "CT"
            };

            await _context.Transactions.AddRangeAsync(transactionReciever, transactionSender);
            await _context.SaveChangesAsync();
        }
    }
}
