using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApp.Application.Queries.GetTransactionsByUserId
{
    class GetTransactionsByUserIdHandler
    {
        private readonly BankAppContext _context;
        public GetTransactionsByUserIdHandler(BankAppContext context)
        {
            _context = context;
        }

        public GetTransactionsByUserIdResponse Handle(GetTransactionByUserIdRequest request)
        {
            var query = _context.Transactions.Where(x => x.AccountId == request.AccountId);

            var response = new GetTransactionsByUserIdResponse()
            {
                Transactions = query
            };

            return response;
        }
    }
}
