using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Queries;
using BankApp.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpGet]
        [Route("{accountid}")]
        public IActionResult Get(int accountid, int? amount, int? page)
        {
            var request = new GetAccountTransactionsRequest { AccountId = accountid, Amount = amount ?? 10, Page = page ?? 0 };

            var handler = new GetAccountTransactionsHandler(new BankContext()).Handler(request);

            return Ok(handler.Account);
        }
    }
}