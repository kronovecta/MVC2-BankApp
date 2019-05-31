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
using MediatR;
using System.Threading;

namespace BankApp.Application.Queries
{
    public class GetCustomersByNamesCityHandler : IRequestHandler<GetCustomerByNamesCityRequest, GetCustomerByNamesCityResponse>
    {
        private readonly IBankContext _context;

        public GetCustomersByNamesCityHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerByNamesCityResponse> Handle(GetCustomerByNamesCityRequest request, CancellationToken cancellationToken)
        {
            var response = new GetCustomerByNamesCityResponse();

            IQueryable<Customer> query;

            if (request.City != null && request.Search != null)
            {
                if(request.Surname == null)
                {
                    query = _context.Customers.OrderBy(x => x.CustomerId)
                    .Where(c => c.City.ToLower() == request.City.ToLower())
                    .Where(x => x.Givenname.ToLower() == request.Search.ToLower() || x.Surname.ToLower() == request.Search.ToLower());
                } else
                {
                    query = _context.Customers.OrderBy(x => x.CustomerId)
                    .Where(c => c.City.ToLower() == request.City.ToLower())
                    .Where(x => x.Givenname.ToLower() == request.Search.ToLower());
                }
                

            }
            else if (request.Search == null && request.City != null)
            {
                query = _context.Customers.OrderBy(x => x.CustomerId)
                    .Where(c => c.City.ToLower().Contains(request.City.ToLower()));
            }
            else
            {
                if(request.Surname == null)
                {
                    query = _context.Customers.OrderBy(x => x.CustomerId).Where(x => x.Givenname.StartsWith(request.Search.ToLower()) || x.Surname.StartsWith(request.Search.ToLower()));
                } else
                {
                    query = _context.Customers.OrderBy(x => x.CustomerId).Where(x => x.Givenname.StartsWith(request.Search.ToLower()) && x.Surname.StartsWith(request.Surname.ToLower()));
                }
                
            }

            if (query != null)
            {
                response.TotalCustomerAmount = query.Count();
                response.TotalNumberOfPages = (response.TotalCustomerAmount / (request.Limit));
                if (request.Limit <= 0)
                {
                    response.Customers = query.ProjectTo<CustomerDto>().ToList();
                }
                else
                {
                    response.Customers = query.Skip(request.Offset).Take(request.Limit).ProjectTo<CustomerDto>().ToList();
                }
            }

            if (response != null)
            {
                return response;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
