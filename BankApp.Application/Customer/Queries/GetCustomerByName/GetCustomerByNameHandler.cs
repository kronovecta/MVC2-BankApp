using BankApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
                .Select(c => new GetCustomerByNameResponse.CustomerDto()
                {
                    Id = c.CustomerId,
                    Firstname = c.Givenname,
                    Lastname = c.Surname,
                    City = c.City
                })
                .OrderBy(x => x.Id)
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
