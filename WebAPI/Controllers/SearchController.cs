using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Application.GetCustomerByName;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using WebAPI.Attributes;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ControllerBase
    {
        private readonly IMapper _mapper;

        public SearchController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> Get(string name, int limit, int pagenr)
        {
            var request = new GetCustomerByNameRequest()
            {
                Search = name,
                Limit = limit,
                Offset = (pagenr * (limit + 1))
            };

            var query = new GetCustomersByNameHandler().Handler(request);

            return Ok(query.Customers.ToList());
        }
    }
}
