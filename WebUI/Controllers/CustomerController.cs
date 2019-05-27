using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;
using WebUI.ViewModels.Account;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDataProtector _protector;

        public CustomerController(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("TokenConverter");
        }
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

                var request = new GetCustomerByNamesCityRequest()
                {
                    City = city,
                    Search = name,
                    Limit = amount,
                    Offset = (amount * pagenr)
                };

                var response = new GetCustomersByNamesCityHandler().Handler(request);

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

        public IActionResult SearchCustomerById(SearchCustomerViewModel model)
        {
            var request = new GetCustomerByIdRequest() { Id = model.CustomerId };

            var response = new GetCustomerByIdHandler().Handler(request);

            model.Customers.Add(response.Customer);
            model.TotalCustomers = 1;
            model.TotalPages = 1;

            return PartialView("_CustomerListPartial", model);
        }

        public IActionResult ShowAccount(int accountid, int? amount, int? pagenr)
        {
            if(ModelState.IsValid)
            {
                var amountFallback = 20;

                var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                var request = new GetAccountByIdRequest()
                {
                    AccountId = accountid
                };

                var request_transaction = new GetTransactionsByAccountIdRequest()
                {
                    AccountId = accountid,
                    Amount = amount ?? amountFallback,
                    Offset = (amountFallback * pagenr) ?? 0
                };

                var query = new GetAccountByIdHandler().Handler(request);
                var transactions = new GetTransactionsByAccountIdHandler().Handler(request_transaction);

                if (query != null)
                {
                    var model = new AccountTransactionsViewModel()
                    {
                        PageNr = pagenr ?? 0,
                        AccountId = accountid,
                        Account = query.Account,
                        Transactions = transactions.Transactions,
                        TotalTransactions = transactions.TotalTransactions,
                        TotalPages = transactions.TotalPages
                    };

                    if (isAjax)
                    {
                        return PartialView("_TransactionListPartial", model);
                    }
                    else
                    {
                        return View(model);
                    }
                }
            }

            return NotFound();
        }

        public IActionResult GetToken(int customerid)
        {
            var token = _protector.Protect(customerid.ToString());

            return PartialView("_TokenPartial", token);
        }
    }
}