using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Entities;

namespace BankApp.Application.Queries.GetSingleCustomer
{
    public class GetCustomerByIdHandler
    {
        private readonly BankAppContext _context;

        public GetCustomerByIdHandler()
        {
            _context = new BankAppContext();
        }

        public GetCustomerByIdResponse Handler(GetCustomerByIdRequest request)
        {
            var query = _context.Customers
                .AsNoTracking()
                .Where(c => c.CustomerId == request.Id)

                .Include(d => d.Dispositions)

                .Include(d => d.Dispositions)
                    .ThenInclude(a => a.Account)
                        .ThenInclude(l => l.Loans)

                .Include(d => d.Dispositions)
                    .ThenInclude(a => a.Account)
                        .ThenInclude(po => po.PermenentOrder)

                .Include(d => d.Dispositions)
                    .ThenInclude(c => c.Cards)
                .SingleOrDefault(x => x.CustomerId.Equals(request.Id));

            var response = new GetCustomerByIdResponse
            {
                Customer = new GetCustomerByIdResponse.CustomerDto()
                {
                    Id = query.CustomerId,
                    FirstName = query.Givenname,
                    LastName = query.Surname,
                    City = query.City,
                    Dispositions = query.Dispositions
                }
            };

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
