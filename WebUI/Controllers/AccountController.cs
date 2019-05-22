using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.Application.Queries;
using BankApp.Application.DtoObjects;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult ShowAccount(int accountid)
        {
            var request = new GetAccountByIdRequest()
            {
                AccountId = accountid
            };

            var query = new GetAccountByIdHandler().Handler(request).Account;

            return View(query);
        }
    }
}