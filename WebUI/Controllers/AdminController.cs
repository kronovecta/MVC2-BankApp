using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using BankApp.Domain.Entities;
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

        [Route("Admin/EditCustomer/{customerid}")]
        public IActionResult EditCustomer(int customerid)
        {
            var request = new GetCustomerByIdRequest()
            {
                Id = customerid
            };

            var query = new GetCustomerByIdHandler().Handler(request).Customer;

            return View(query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/EditCustomer/{customerid}")]
        public IActionResult EditCustomer(CustomerDto model)
        {
            // Dummy EditCustomer command
            var command = new UpdateCustomerCommand()
            {
                Customer = model
            };

            var handler = new UpdateCustomerHandler().Handler(command);

            if(handler.IsCompletedSuccessfully)
            {
                return View();
            } else
            {
                return View(model);
            }
            
        }
    }
}