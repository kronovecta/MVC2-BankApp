using AutoMapper;
using BankApp.Application.DtoObjects;
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
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly BankContext _context;

        public UpdateCustomerHandler(BankContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.CustomerId == command.CustomerId);

            customer.Gender = command.Gender;
            customer.Givenname = command.Givenname;
            customer.Surname = command.Surname;
            customer.Streetaddress = command.Streetaddress;
            customer.City = command.City;
            customer.Country = command.Country;
            customer.CountryCode = command.CountryCode;
            customer.Birthday = command.Birthday;
            customer.NationalId = command.NationalId;
            customer.Telephonecountrycode = command.Telephonecountrycode;
            customer.Telephonenumber = command.Telephonenumber;
            customer.Emailaddress = command.Emailaddress;

            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}
