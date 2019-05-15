using BankApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;

namespace BankApp.Application.GetCustomerByName
{
    public class GetCustomerByNameHandler
    {
        private readonly BankContext _context;

        public GetCustomerByNameHandler()
        {
            _context = new BankContext();
        }

        public GetCustomerByNameResponse Handler(GetCustomerByNameRequest request)
        {
            var response = new GetCustomerByNameResponse();

            var query = _context.Customers.OrderBy(x => x.CustomerId).Where(x => x.Givenname.StartsWith(request.Search.ToLower()));

            response.TotalCustomerAmount = query.Count();
            response.TotalNumberOfPages = (response.TotalCustomerAmount / (request.Limit + 1));
            response.Customers = query
                .AsNoTracking()
                .Skip(request.Limit*(request.Offset - 1))
                .Take(request.Limit)
                .Select(c => new CustomerDto()
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    City = c.City
                })
                .OrderBy(x => x.CustomerId)
                .ToList();
            
            if(response != null)
            {
                return response;
            } else
            {
                throw new NullReferenceException();
            }
        }
    }
}
