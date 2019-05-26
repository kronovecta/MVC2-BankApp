using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Commands;
using BankApp.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels.Account;

namespace WebUI.Controllers
{
    public class CashierController : Controller
    {
        public IActionResult CashierPanel()
        {
            return View();
        }

        #region Deposit
        public IActionResult Deposit(int accountid)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(DepositViewModel model)
        {
            if(ModelState.IsValid)
            {
                var command = new DepositCommand()
                {
                    AccountId = model.AccountId,
                    Amount = model.Amount
                };

                var query = new DepositHandler().Handler(command);
                if (query.IsCompletedSuccessfully)
                {
                    TempData["Success"] = $"{model.Amount.ToString("C")} deposited to account";
                    return View();
                }
                else
                {
                    TempData["Error"] = $"Deposit failed";
                    return View();
                }
            } else
            {
                return NotFound();
            }
        }
        #endregion
        #region Withdraw
        public IActionResult Withdraw(int accountid)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(WithdrawViewModel model)
        {
            return View();
        }
        #endregion

        #region Transfer
        public IActionResult Transfer(int accountid)
        {
            var request = new GetAccountByIdRequest { AccountId = accountid };

            var query = new GetAccountByIdHandler().Handler(request).Account;

            var model = new TransferViewModel { AccountIdSender = accountid, BalanceSender = query.Balance };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(TransferViewModel model)
        {
            //var command = new TransferCommand
            //{
            //    AccountId_Sender = model.AccountIdSender,
            //    AccountId_Reciever = model.AccountIdReciever,
            //    Amount = model.Amount
            //};

            var command = new TransferCommand
            {
                AccountId_Sender = 1,
                AccountId_Reciever = 9,
                Amount = 500
            };

            var query = new TransferHandler().Handler(command);

            return View();
        }
        #endregion
    }
}