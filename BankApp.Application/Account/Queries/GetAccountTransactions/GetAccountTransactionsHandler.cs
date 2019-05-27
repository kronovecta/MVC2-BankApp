using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountTransactionsHandler
    {
        private readonly BankContext _context;

        public GetAccountTransactionsHandler(BankContext context)
        {
            _context = context;
        }

        public GetAccountTransactionsResponse Handler(GetAccountTransactionsRequest request)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.AccountId == request.AccountId);
            var transactions = _context.Transactions.Where(t => t.AccountId == request.AccountId);

            var offset = request.Page * request.Amount;

            var response = new GetAccountTransactionsResponse();
            response.Account = Mapper.Map<Account, AccountDto>(account);
            response.Account.Transactions = Mapper.Map<List<Transaction>, List<TransactionDto>>(transactions.Skip(offset).Take(request.Amount).ToList());

            return response;
        }
    }
}
