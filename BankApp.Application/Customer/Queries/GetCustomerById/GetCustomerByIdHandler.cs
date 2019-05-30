using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Queries
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
    {
        private readonly BankContext _context;

        public GetCustomerByIdHandler(BankContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault(y => y.CustomerId == request.Id);
            var accounts = (from a in _context.Accounts
                            join d in _context.Dispositions on a.AccountId equals d.AccountId
                            join c in _context.Customers on d.CustomerId equals c.CustomerId
                            where c.CustomerId == request.Id
                            select a);

            return new GetCustomerByIdResponse()
            {
                Customer = Mapper.Map<Customer, CustomerDto>(query),
                Accounts = Mapper.Map<List<Account>, List<AccountDto>>(accounts.ToListAsync().Result)
            };
        }
    }
}
