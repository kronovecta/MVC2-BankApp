using BankApp.Application.Commands;
using BankApp.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WebUI.ViewModels.Account;

namespace WebUI.Controllers
{
    [Authorize(Roles = "cashier")]
    public class CashierController : Controller
    {
        private readonly IMediator _mediator;

        public CashierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult CashierPanel()
        {
            return View();
        }

        #region Deposit
        public IActionResult Deposit()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(DepositViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Amount.Contains('.'))
                {
                    model.Amount = model.Amount.Replace('.', ',');
                }

                var value = Decimal.Parse(model.Amount, CultureInfo.CurrentCulture);

                var command = new DepositCommand()
                {
                    AccountId = model.AccountId,
                    Amount = value
                };

                //var query = new DepositHandler().Handler(command);
                var query = await _mediator.Send(command);

                TempData["Success"] = $"{value.ToString("C")} deposited to account";
                return RedirectToAction("ShowAccount", "Customer", new { accountid = model.AccountId });
            }

            return View();
        }
        #endregion
        #region Withdraw
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(WithdrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Amount.Contains('.'))
                {
                    model.Amount = model.Amount.Replace('.', ',');
                }

                var value = Decimal.Parse(model.Amount, CultureInfo.CurrentCulture);

                var command = new WithdrawCommand
                {
                    AccountId = model.AccountId,
                    Amount = value,
                };

                var query = _mediator.Send(command);

                if (query.IsCompletedSuccessfully)
                {
                    return RedirectToAction("ShowAccount", "Customer", new { accountid = model.AccountId });
                }
                else
                {
                    TempData["failure"] = query.Exception.InnerException.Message;
                    return View();
                }

            }

            return View();
        }
        #endregion

        #region Transfer
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Amount.Contains('.'))
                {
                    model.Amount = model.Amount.Replace('.', ',');
                }

                var value = Decimal.Parse(model.Amount, CultureInfo.CurrentCulture);

                var command = new TransferCommand
                {
                    AccountId_Sender = model.AccountIdSender,
                    AccountId_Reciever = model.AccountIdReciever,
                    Amount = value
                };

                //var query = new TransferHandler(new BankContext()).Handler(command);
                var query = _mediator.Send(command);

                if(query.IsCompletedSuccessfully)
                {
                    return RedirectToAction("ShowAccount", "Customer", new { accountid = model.AccountIdReciever });
                } else
                {
                    TempData["failure"] = query.Exception.InnerException.Message;
                    return View();
                }
                
            }

            return View();
        }
        #endregion

        #region Interest
        public IActionResult ApplyInterest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplyInterest(InterestViewModel model)
        {
            var command = new ApplyInterestCommand { AccountId = 1, Amount = 1, PreviousApplication = DateTime.Parse("2018-05-28") };
            var query = _mediator.Send(command);

            TempData["success"] = "Interest applied succesfully";
            return View();
        }
        #endregion
    }
}