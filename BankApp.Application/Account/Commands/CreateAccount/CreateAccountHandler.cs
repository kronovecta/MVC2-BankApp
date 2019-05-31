using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IBankContext _context;
        public CreateAccountHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = new Domain.Entities.Account
            {
                Frequency = command.Frequency,
                Created = command.Created
            };

            await _context.Accounts.AddAsync(account);

            return Unit.Value;
        }
    }
}
