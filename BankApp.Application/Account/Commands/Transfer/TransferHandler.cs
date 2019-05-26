using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class TransferHandler
    {
        private readonly BankContext _context;
        public TransferHandler()
        {
            _context = new BankContext();
        }

        public async Task Handler(TransferCommand command)
        {
            var accountSender = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Sender);
            var accountReciever = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Reciever);

            if(accountSender.Balance >= command.Amount)
            {
                accountSender.Withdraw(command.Amount);
                accountReciever.Deposit(command.Amount);

                var transactionCommand = new CreateTransferTransactionCommand()
                {
                    Amount = command.Amount,
                    Reciever = accountReciever,
                    Sender = accountSender
                };

                var query = new CreateTransferTransactionHandler().Handler(transactionCommand);

                await _context.SaveChangesAsync();
            }
        }
    }
}
