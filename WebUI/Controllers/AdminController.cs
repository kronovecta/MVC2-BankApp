using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Application.Commands;
using BankApp.Application.DtoObjects;
using BankApp.Application.Identity;
using BankApp.Application.Queries;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels.Identity;

namespace WebUI.Controllers
{
    [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand customer)
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

            var result = await _mediator.Send(command);
            var latest = await _mediator.Send(new GetLatestCustomerRequest());

            return RedirectToAction("ShowCustomer", "Customer", new { id = latest.CustomerId });
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditCustomer(int customerid)
        {
            if(customerid != 0)
            {
                var request = new GetCustomerByIdRequest()
                {
                    Id = customerid
                };

                var customer = _mediator.Send(request).Result.Customer;

                var command = new CustomerDto
                {
                    CustomerId = customer.CustomerId,
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

                return View(command);
            } else
            {
                return RedirectToAction("UserList", "Admin");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCustomer(CustomerDto customer)
        {
            var command = new UpdateCustomerCommand
            {
                CustomerId = customer.CustomerId,
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

            var query = _mediator.Send(command);

            if(query.Exception == null)
            {
                return RedirectToAction("ShowCustomer", "Customer", new { id = command.CustomerId });
            } else
            {
                return View(customer);
            }
            
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var command = await _mediator.Send(new CreateUserCommand { Username = model.Username, Email = model.Email, Password = model.Password, Phone = model.Phone, Role = model.Role });
            var account = await _mediator.Send(new CreateAccountCommand { Balance = 0, Created = DateTime.Now, Frequency = "Monthly" });

            TempData["success"] = "User created succesfullly";
            return RedirectToAction("CreateUser", "Admin");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UserList()
        {
            var model = await _mediator.Send(new GetUsersCommand());

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUser(string userid)
        {
            if(userid != null || userid != "")
            {
                var user = await _mediator.Send(new GetUserByIdCommand { Id = userid });
                var roles = await _mediator.Send(new GetRolesRequest());

                var model = new UpdateUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.Username,
                    Phone = user.Phone,
                    Role = user.Role,
                    Roles = roles.Roles
                };

                return View(model);
            }

            return RedirectToAction("UserList", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUser(UpdateUserViewModel model)
        {
            var result = await _mediator.Send(new UpdateUserCommand { Email = model.Email, Phone = model.Phone, UserName = model.UserName, UserId = model.Id, Role = model.Role });

            return RedirectToAction("UserList", "Admin");
        }
    }
}