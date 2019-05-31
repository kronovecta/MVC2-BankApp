using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MeController : Controller
    {
        private readonly IMediator _mediator;
        private IDataProtector _protector;
       
        public MeController(IMediator mediator, IDataProtectionProvider provider)
        {
            _mediator = mediator;
            _protector = provider.CreateProtector("MeController");
        }

        [JwtAuthentication]
        public async Task<IActionResult> Get(string token)
        {
            var id = int.Parse(_protector.Unprotect(token));
            var result = await _mediator.Send(new GetCustomerByIdRequest() { Id = id });

            return Ok(result);
        }
    }
}