using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult ShowCustomer(int id)
        {
            var model = new GetCustomerByIdHandler().Handler(new GetCustomerByIdRequest() { Id = id }).Customer;

            return View(model);
        }

        public IActionResult SearchCustomer()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SearchCustomerByName(SearchCustomerViewModel model, int? pagenr)
        public IActionResult SearchCustomerByName(string name, string city, int amount, int pagenr)
        {
            if(ModelState.IsValid)
            {
                var model = new SearchCustomerViewModel()
                {
                    Name = name,
                    Amount = amount,
                    PageNr = pagenr
                };

                var request = new GetCustomerByNameRequest()
                {
                    Search = name,
                    Limit = amount,
                    Offset = (amount * pagenr)
                };

                var response = new GetCustomersByNameHandler().Handler(request);

                model.Name = name;
                model.Amount = amount;
                model.PageNr = pagenr;

                model.Customers = response.Customers;
                model.TotalCustomers = response.TotalCustomerAmount;
                model.TotalPages = response.TotalNumberOfPages;

                return PartialView("_CustomerListPartial", model);
            }

            return NotFound();
        }
    }
}