using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;
using BankApp.Application.GetCustomerByName;
using BankApp.Application.Queries.GetSingleCustomer;
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
            var model = new SearchCustomerViewModel() { Customers = new List<CustomerDto>() };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SearchCustomerByName(SearchCustomerViewModel model, int? pagenr)
        {
            if(ModelState.IsValid)
            {
                var request = new GetCustomerByNameRequest()
                {
                    Search = model.Name,
                    Limit = model.Amount,
                    Offset = (model.Amount * model.PageNr)
                };

                var response = new GetCustomersByNameHandler().Handler(request);

                model.Customers = response.Customers;   
                //model.PageNr = PageNr++;
                model.TotalCustomers = response.TotalCustomerAmount;
            }

            return PartialView("_CustomerListPartial", model);
        }
    }
}