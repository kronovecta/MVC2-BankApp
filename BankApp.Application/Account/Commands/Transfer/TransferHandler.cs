using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
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
    public class TransferHandler : IRequestHandler<TransferCommand>
    {
        private readonly BankContext _context;
        public TransferHandler(BankContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(TransferCommand command, CancellationToken cancellationToken)
        {
            if (command.Amount > 0)
            {
                var accountSender = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Sender);
                var accountReciever = _context.Accounts.SingleOrDefault(x => x.AccountId == command.AccountId_Reciever);

                if (accountSender.Balance >= command.Amount)
                {
                    accountSender.Balance -= command.Amount;
                    accountReciever.Balance += command.Amount;

                    var transactionSender = new Transaction()
                    {
                        AccountId = accountSender.AccountId,
                        Date = DateTime.Now,
                        Type = "Debit",
                        Operation = $"Transfer to {accountSender.AccountId}",
                        Amount = command.Amount,
                        Balance = accountSender.Balance,
                        Bank = "CT"
                    };

                    var transactionReciever = new Transaction()
                    {
                        AccountId = accountReciever.AccountId,
                        Date = DateTime.Now,
                        Type = "Debit",
                        Operation = $"Transfer from {accountReciever.AccountId}",
                        Amount = command.Amount,
                        Balance = accountReciever.Balance,
                        Bank = "CT"
                    };

                    await _context.Transactions.AddRangeAsync(transactionSender, transactionReciever);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InsufficientFundsException();
                }
            }
            else
            {
                throw new NegativeAmountException();
            }

            return Unit.Value;
        }
    }
}
