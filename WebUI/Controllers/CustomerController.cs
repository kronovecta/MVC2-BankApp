using BankApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebUI.ViewModels;
using WebUI.ViewModels.Account;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDataProtector _protector;

        public CustomerController(IDataProtectionProvider provider, IMediator mediator)
        {
            _mediator = mediator;
            _protector = provider.CreateProtector("TokenConverter");
        }
        public async Task<IActionResult> ShowCustomer(int id)
        {
            var model = await _mediator.Send(new GetCustomerByIdRequest { Id = id });

            if(model.Customer != null)
            {
                model.Customer.TotalBalance = model.Accounts.Sum(x => x.Balance);
                return View(model);
            } else
            {
                return RedirectToAction("SearchCustomer", "Customer");
            }
        }

        public IActionResult SearchCustomer()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SearchCustomerByName(string name, string city, int amount, int pagenr)
        {
            if (ModelState.IsValid)
            {
                var givenname = "";
                var surname = "";

                if(name.Split(" ").Length > 1)
                {
                    var names = name.Split(" ");
                    givenname = names[0];
                    surname = names[1];
                } else
                {
                    givenname = name;
                }

                var model = new SearchCustomerViewModel()
                {
                    Name = givenname,
                    Surname = surname,
                    Amount = amount,
                    PageNr = pagenr
                };

                var request = new GetCustomerByNamesCityRequest()
                {
                    City = city,
                    Search = givenname,
                    Surname = surname,
                    Limit = amount,
                    Offset = (amount * pagenr)
                };

                //var response = new GetCustomersByNamesCityHandler().Handler(request);
                var response = _mediator.Send(request);

                if(response.IsCompletedSuccessfully)
                {
                    model.Name = name;
                    model.Amount = amount;
                    model.PageNr = pagenr;
                    model.City = city;

                    model.Customers = response.Result.Customers;
                    model.TotalCustomers = response.Result.TotalCustomerAmount;
                    model.TotalPages = response.Result.TotalNumberOfPages;
                }

                return PartialView("_CustomerListPartial", model);
            }

            return NotFound();
        }

        public IActionResult SearchCustomerById(SearchCustomerViewModel model)
        {
            var request = new GetCustomerByIdRequest() { Id = model.CustomerId };
            var response = _mediator.Send(request);

            model.Customers.Add(response.Result.Customer);
            model.TotalCustomers = 1;
            model.TotalPages = 1;

            return PartialView("_CustomerListPartial", model);
        }

        public async Task<IActionResult> ShowAccount(int accountid, int? amount, int? pagenr)
        {
            if (ModelState.IsValid)
            {
                var amountFallback = 20;

                var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                var request = new GetAccountTransactionsRequest { AccountId = accountid, Amount = amount ?? amountFallback, Page = pagenr ?? 0};

                var query = await _mediator.Send(request);

                var model = new AccountTransactionsViewModel()
                {
                    PageNr = pagenr ?? 0,
                    NextPage = pagenr++ ?? 0,
                    AccountId = accountid,
                    Account = query.Account,
                    Transactions = query.Transactions,
                    TotalTransactions = query.TotalTransactions,
                    TotalPages = query.TotalPages
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

            return NotFound();
        }

        public IActionResult GetToken(int customerid)
        {
            var token = _protector.Protect(customerid.ToString());

            return PartialView("_TokenPartial", token);
        }
    }
}