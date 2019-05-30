using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Queries;
using BankApp.Data;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{accountid}")]
        public async Task<IActionResult> Get(int accountid, int? amount, int? page)
        {
            var model = await _mediator.Send(new GetAccountTransactionsRequest { AccountId = accountid, Amount = amount ?? 10, Page = page ?? 0 });
            return Ok(model);
        }
    }
}