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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/EditCustomer/{customerid}")]
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
            var account = await _mediator.Send(new CreateAccountCommand { Balance = 0, Created = DateTime.Now, Frequency = "Monthly" });

            TempData["success"] = "User created succesfullly";
            return RedirectToAction("CreateUser", "Admin");
        }

        public async Task<IActionResult> UserList()
        {
            var model = await _mediator.Send(new GetUsersCommand());

            return View(model);
        }

        public async Task<IActionResult> UpdateUser(string userid)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            var result = await _mediator.Send(new UpdateUserCommand { Email = model.Email, Phone = model.Phone, UserName = model.UserName, UserId = model.Id, Role = model.Role });

            return RedirectToAction("UserList", "Admin");
        }
    }
}