using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Application.DtoObjects;

namespace BankApp.Application.Queries.GetAccount
{
    class GetAccountHandler
    {
        private readonly BankContext _context;

        public GetAccountHandler()
        {
            _context = new BankContext();
        }

        public GetAccountResponse Handler(GetAccountRequest request)
        {
            //var query = _context.Accounts.Where(x => x.Dispositions.Contains(disposition))
            var query = (from a in _context.Accounts join d in _context.Dispositions on a.AccountId equals d.AccountId where d.CustomerId == request.CustomerId select a);

            var response = new GetAccountResponse();
            response.Accounts = query
                .AsNoTracking()
                .Select(a => new AccountDto()
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Created = a.Created,
                    Balance = a.Balance
                })
                .OrderBy(x => x.AccountId)
                .ToList();

            return response;
        }
    }
}
