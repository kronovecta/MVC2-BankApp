using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using MediatR;
using BankApp.Application.Queries;
using Microsoft.AspNetCore.DataProtection;

namespace WebAPI.Controllers
{
    public class TokenController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IDataProtector _protector;

        public TokenController(IMediator mediator, IDataProtectionProvider provider)
        {
            _mediator = mediator;
            _protector = provider.CreateProtector("TokenConverter");
        }

        [AllowAnonymous]
        public ActionResult<string> Get(int id)
        {
            var token = _protector.Protect(id.ToString());
            return token;
            //return JwtManager.GenerateToken(id);

            //throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        //private async Task<bool> CheckUser(int id)
        //{
        //    var result = await _mediator.Send(new GetCustomerByIdRequest { Id = id });

        //    throw new NotImplementedException();
        //}
    }
}