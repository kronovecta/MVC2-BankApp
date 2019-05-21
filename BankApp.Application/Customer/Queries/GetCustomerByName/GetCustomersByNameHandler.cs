using BankApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BankApp.Application.DtoObjects;
using AutoMapper;
using BankApp.Domain.Entities;
using AutoMapper.QueryableExtensions;

namespace BankApp.Application.GetCustomerByName
{
    public class GetCustomersByNameHandler
    {
        private readonly BankContext _context;

        public GetCustomersByNameHandler()
        {
            _context = new BankContext();
        }

        public GetCustomerByNameResponse Handler(GetCustomerByNameRequest request)
        {
            var response = new GetCustomerByNameResponse();

            var query = _context.Customers.OrderBy(x => x.CustomerId).Where(x => x.Givenname.StartsWith(request.Search.ToLower()));

            response.TotalCustomerAmount = query.Count();
            response.TotalNumberOfPages = (response.TotalCustomerAmount / (request.Limit + 1));
            if(request.Limit <= 0)
            {
                response.Customers = query.ProjectTo<CustomerDto>().ToList();
            } else
            {
                response.Customers = query.Skip(request.Offset).Take(request.Limit).ProjectTo<CustomerDto>().ToList();
            }
            
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
