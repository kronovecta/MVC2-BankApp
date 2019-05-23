using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.Application.Queries;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IActionResult Get(int accountid)
        {
            var request = new GetTransactionsByAccountIdRequest()
            {
                AccountId = accountid
            };

            var query = new GetTransactionsByAccountIdHandler().Handler(request);

            if (query != null)
            {
                return Ok(query);
            }
            else
            {
                return NotFound();
            }
        }
    }
}