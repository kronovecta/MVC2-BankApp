using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Queries.GetSingleCustomer;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult ShowCustomer(int id)
        {
            var model = new GetCustomerByIdHandler().Handler(new GetCustomerByIdRequest() { Id = id }).Customer;

            return View(model);
        }
    }
}