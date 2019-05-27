using BankApp.Application.Commands;
using BankApp.Data;
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
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(DepositViewModel model)
        {
            if (ModelState.IsValid)
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
            }
            else
            {
                return NotFound();
            }
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
                var command = new WithdrawCommand
                {
                    AccountId = model.AccountId,
                    Amount = model.Amount,
                };

                var query = new WithdrawHandler(new BankContext()).Handler(command);

                if (query.IsCompletedSuccessfully)
                {
                    TempData["Success"] = $"{model.Amount.ToString("C")} withdrawn from account";
                    return View();
                }
                else
                {
                    TempData["Error"] = $"Withdraw failed";
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
                var command = new TransferCommand
                {
                    AccountId_Sender = model.AccountIdSender,
                    AccountId_Reciever = model.AccountIdReciever,
                    Amount = model.Amount
                };

                var query = new TransferHandler(new BankContext()).Handler(command);

                if (query.IsCompletedSuccessfully)
                {
                    TempData["Success"] = $"{model.Amount.ToString("C")} transferred from {model.BalanceSender} to {model.AccountIdReciever}";
                    return View();
                }
                else
                {
                    TempData["Error"] = $"Transfer failed";
                    return View();
                }
            }

            return NotFound();
        }
        #endregion
    }
}