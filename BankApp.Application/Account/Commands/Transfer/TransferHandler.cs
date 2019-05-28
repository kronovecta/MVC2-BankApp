using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using BankApp.Application.Exceptions;
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
        public TransferHandler(BankContext context)
        {
            _context = context;
        }

        public async Task Handler(TransferCommand command)
        {
            if(command.Amount > 0)
            {
                var accountSender = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Sender);
                var accountReciever = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Reciever);

                if (accountSender.Balance >= command.Amount)
                {
                    accountSender.Balance -= command.Amount;
                    accountReciever.Balance += command.Amount;

                    var transactionCommand = new CreateTransferTransactionCommand()
                    {
                        Amount = command.Amount,
                        Reciever = accountReciever,
                        Sender = accountSender
                    };

                    //var query = new CreateTransferTransactionHandler().Handler(transactionCommand); // No Handler in Handler

                    await _context.SaveChangesAsync();
                } else
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
