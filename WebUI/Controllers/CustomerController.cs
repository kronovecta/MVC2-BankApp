using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;
using WebUI.ViewModels.Account;

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

        public IActionResult ShowAccount(int accountid)
        {
            var request = new GetAccountByIdRequest()
            {
                AccountId = accountid,
                Amount = 50
            };

            var request_transaction = new GetTransactionsByAccountIdRequest()
            {
                AccountId = accountid,
                Amount = 50,
                Offset = 0
            };

            var query = new GetAccountByIdHandler().Handler(request);
            var transactions = new GetTransactionsByAccountIdHandler().Handler(request_transaction);
            
            if(query != null)
            {
                var model = new AccountTransactionsViewModel()
                {
                    PageNr = 0,
                    Account = query.Account,
                    Transactions = transactions.Transactions,
                    TotalTransactions = transactions.Transactions.Count(),
                    TotalPages = transactions.Transactions.Count() / request.Amount
                };

                return View(model);
            }

            return NotFound();
        }

        public IActionResult GetTransactions(int accountid, int amount, int pagenr)
        {
            if(ModelState.IsValid)
            {
                var request = new GetTransactionsByAccountIdRequest()
                {
                    AccountId = accountid,
                    Amount = amount,
                    Offset = amount * pagenr
                };

                var query = new GetTransactionsByAccountIdHandler().Handler(request);

                if (query != null)
                {
                    var model = new AccountTransactionsViewModel()
                    {
                        PageNr = pagenr,
                        Account = null,
                        AccountId = accountid,
                        TotalTransactions = query.TotalTransactions,
                        TotalPages = query.TotalPages,
                        Transactions = query.Transactions
                    };

                    return PartialView("_TransactionListPartial", model);
                }
                else
                {
                    return NotFound();
                }
            }

            return NotFound();
        }
    }
}