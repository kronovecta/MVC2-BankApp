using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Application.Queries;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using WebAPI;

namespace BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> Get()
        {
            return null;
        }

        //[HttpGet("{name}/{limit}")]
        //[HttpGet("{name}")]
        //public ActionResult<IEnumerable<Customer>> Get(string name, int limit)
        //{
        //    //var query = utils.GetCustomersNamed(name, limit);
        //    var request = new GetCustomerByNameRequest()
        //    {
        //        Search = name,
        //        Limit = limit
        //    };

        //    var query = new GetCustomersByNameHandler().Handler(request);

        //    return Ok(query.Customers.ToList());
        //}

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var request = new GetCustomerByIdRequest()
            {
                Id = id
            };


            //var query = new GetCustomerByIdHandler().Handler(request).Customer;
            var query = _mediator.Send(request);

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

    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDataProtector _protector;

        public MeController(IDataProtectionProvider provider, IMediator mediator)
        {
            _mediator = mediator;
            _protector = provider.CreateProtector("TokenConverter");
        }

        [JwtAuthentication]
        public ActionResult<CustomerDto> Get(string token)
        {
            var unprotected = _protector.Unprotect(token);
            var result = int.TryParse(unprotected, out int id);
            if (result)
            {
                var request = new GetCustomerByIdRequest { Id = id };

                var query = _mediator.Send(request);

                if (query.Result.Customer != null)
                {
                    return Ok(query.Result);
                }
            }

            return NotFound();
        }
    }
}