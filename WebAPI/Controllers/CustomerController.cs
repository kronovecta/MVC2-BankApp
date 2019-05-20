using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Application.GetCustomerByName;
using BankApp.Application.Queries.GetSingleCustomer;
using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
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
        public ActionResult<Customer> Get(int id)
        {
            var request = new GetCustomerByIdRequest()
            {
                Id = id
            };


            var query = new GetCustomerByIdHandler().Handler(request).Customer;

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