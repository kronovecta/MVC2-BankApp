using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class UpdateCustomerHandler
    {
        private readonly BankContext _context;

        public UpdateCustomerHandler()
        {
            _context = new BankContext();
        }

        public async Task Handler(UpdateCustomerCommand command)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.CustomerId == command.Customer.CustomerId);

            customer.Gender = command.Customer.Gender;
            customer.Givenname = command.Customer.Givenname;
            customer.Surname = command.Customer.Surname;
            customer.Streetaddress = command.Customer.Streetaddress;
            customer.City = command.Customer.City;
            customer.Country = command.Customer.Country;
            customer.CountryCode = command.Customer.CountryCode;
            customer.Birthday = command.Customer.Birthday;
            customer.NationalId = command.Customer.NationalId;
            customer.Telephonecountrycode = command.Customer.Telephonecountrycode;
            customer.Telephonenumber = command.Customer.Telephonenumber;
            customer.Emailaddress = command.Customer.Emailaddress;

            await _context.SaveChangesAsync();
        }
    }
}
