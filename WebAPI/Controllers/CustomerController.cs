using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CustomerController()
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return null;
        }

        ////[HttpGet("{name}/{limit}")]
        //[HttpGet("{name}")]
        //public ActionResult<IEnumerable<Customer>> Get(string name, int limit)
        //{
        //    var query = utils.GetCustomersNamed(name, limit);
        //    return Ok(query.customers.ToList());
        //}

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var request = new GetCustomerByIdRequest()
            {
                Id = id
            };

            var query = new GetCustomerByIdHandler().Handler(request);

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