using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels.Identity;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IMediator mediator, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
        }

        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(CreateCustomerCommand customer)
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

            //new CreateCustomerHandler().Handler(command);
            var result = _mediator.Send(command);

            return View();
        }

        [Route("Admin/EditCustomer/{customerid}")]
        public IActionResult EditCustomer(int customerid)
        {
            var request = new GetCustomerByIdRequest()
            {
                Id = customerid
            };

            //var query = new GetCustomerByIdHandler().Handler(request).Customer;
            var query = _mediator.Send(request);

            return View(query.Result.Customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/EditCustomer/{customerid}")]
        public async Task<IActionResult> EditCustomer(UpdateCustomerCommand customer)
        {
            //var command = new UpdateCustomerCommand
            //{
            //    Gender = customer.Gender,
            //    Givenname = customer.Givenname,
            //    Surname = customer.Surname,
            //    Streetaddress = customer.Streetaddress,
            //    City = customer.City,
            //    Zipcode = customer.Zipcode,
            //    Country = customer.Country,
            //    CountryCode = customer.CountryCode,
            //    Birthday = customer.Birthday,
            //    NationalId = customer.NationalId,
            //    Telephonecountrycode = customer.Telephonecountrycode,
            //    Telephonenumber = customer.Telephonenumber,
            //    Emailaddress = customer.Emailaddress
            //};

            //var handler = new UpdateCustomerHandler().Handler(command);
            var query = _mediator.Send(customer);

            if(query.IsCompletedSuccessfully)
            {
                return View();
            } else
            {
                return View(customer);
            }
            
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var command = await _mediator.Send(new CreateUserCommand { Username = model.Username, Email = model.Email, Password = model.Password, Phone = model.Phone, Role = model.Role });

            TempData["success"] = "User created succesfullly";
            return RedirectToAction("CreateUser", "Admin");
        }
    }
}