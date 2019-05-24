using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    class CreateCustomerHandler
    {
        private readonly BankContext _context;
        public CreateCustomerHandler()
        {
            _context = new BankContext();
        }

        public void Handler(CreateCustomerCommand command)
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

            var account = new CreateAccountCommand()
            {
                Balance = 0,
                Frequency = "MONTHLY",
                Created = DateTime.Now
            };



            //_context.Dispositions.Add()
        }
    }
}
