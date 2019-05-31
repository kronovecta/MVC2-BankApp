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
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IBankContext _context;
        public CreateCustomerHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                Gender = command.Gender,
                Givenname = command.Givenname,
                Surname = command.Surname,
                Streetaddress = command.Streetaddress,
                City = command.City,
                Zipcode = command.Zipcode,
                Country = command.Country,
                CountryCode = command.CountryCode,
                Birthday = command.Birthday,
                NationalId = command.NationalId,
                Telephonenumber = command.Telephonenumber,
                Telephonecountrycode = command.Telephonecountrycode,
                Emailaddress = command.Emailaddress
            };

            var account = new Account()
            {
                Frequency = "MONTHLY",
                Created = DateTime.Now,
                Balance = 0
            };

            var disposition = new Disposition()
            {
                Account = account,
                Customer = customer,
                Type = "OWNER"
            };

            await _context.Dispositions.AddAsync(disposition);
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}
