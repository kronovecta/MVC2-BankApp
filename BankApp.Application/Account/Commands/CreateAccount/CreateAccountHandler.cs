using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    class CreateAccountHandler
    {
        private readonly BankContext _context;
        public CreateAccountHandler(BankContext context)
        {
            _context = context;
        } 

        public async Task Handler(CreateAccountCommand command)
        {
            var account = new Domain.Entities.Account
            {
                Frequency = command.Frequency,
                Created = command.Created
            };

            await _context.Accounts.AddAsync(account);
        }
    }
}
