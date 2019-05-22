using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Application.DtoObjects;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BankApp.Application.Queries
{
    public class GetAccountsByUserIdHandler
    {
        private readonly BankContext _context;

        public GetAccountsByUserIdHandler()
        {
            _context = new BankContext();
        }

        public GetAccountsByUserIdResponse Handler(GetAccountsByUserIdRequest request)
        {
            var query = (from a in _context.Accounts join d in _context.Dispositions on a.AccountId equals d.AccountId where d.CustomerId == request.CustomerId select a);

            var response = new GetAccountsByUserIdResponse()
            {
                Accounts = query.ProjectTo<AccountDto>().ToList()
            };

            return response;
        }
    }
}
