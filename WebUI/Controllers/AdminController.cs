using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(CustomerDto customer)
        {
            var command = new CreateCustomerCommand
            {
                Gender = customer.Gender,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Zipcode = customer.Zipcode,
                Country = customer.Country,
                CountryCode = customer.CountryCode,
                Birthday = customer.Birthday,
                NationalId = customer.NationalId,
                Telephonecountrycode = customer.Telephonecountrycode,
                Telephonenumber = customer.Telephonenumber,
                Emailaddress = customer.Emailaddress
            };

            new CreateCustomerHandler().Handler(command);

            return View();
        }
    }
}