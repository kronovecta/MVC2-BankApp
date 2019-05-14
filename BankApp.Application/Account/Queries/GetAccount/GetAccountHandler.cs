using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApp.Application.Queries.GetAccount
{
    class GetAccountHandler
    {
        private readonly BankContext _context;

        public GetAccountHandler(BankContext context)
        {
            _context = context;
        }

        public GetAccountResponse Handle(GetAccountRequest request)
        {
            //var query = _context.Accounts.Where(x => x.Dispositions.Contains(disposition))
            var query = (from a in _context.Accounts join d in _context.Dispositions on a.AccountId equals d.AccountId where d.CustomerId == request.CustomerId select a);

            var response = new GetAccountResponse();

            return response;
        }
    }
}
