using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Queries
{
    public class GetAccountTransactionsHandler : IRequestHandler<GetAccountTransactionsRequest, GetAccountTransactionsResponse>
    {
        private readonly IBankContext _context;

        public GetAccountTransactionsHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<GetAccountTransactionsResponse> Handle(GetAccountTransactionsRequest request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.AccountId == request.AccountId);
            var transactions = _context.Transactions.OrderByDescending(x => x.TransactionId).Where(t => t.AccountId == request.AccountId);

            var offset = request.Page * request.Amount;

            var response = new GetAccountTransactionsResponse()
            {
                Account = Mapper.Map<Account, AccountDto>(account),
                Transactions = Mapper.Map<List<Transaction>, List<TransactionDto>>(transactions.Skip(offset).Take(request.Amount).ToList()),
                TotalTransactions = transactions.Count(),
                TotalPages = transactions.Count() / request.Amount
            };

            return response;
        }
    }
}
