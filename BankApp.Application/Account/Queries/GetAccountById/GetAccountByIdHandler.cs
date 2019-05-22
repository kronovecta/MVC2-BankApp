using AutoMapper;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Application.DtoObjects;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdHandler
    {
        private readonly BankContext _context;

        public GetAccountByIdHandler()
        {
            _context = new BankContext();
        }

        public GetAccountByIdResponse Handler(GetAccountByIdRequest request)
        {
            var response = new GetAccountByIdResponse();

            //var query = (from a in _context.Accounts join t in _context.Transactions on a.AccountId equals t.AccountId select new { Account = a, Transactions = t });
            var query_account = _context.Accounts.SingleOrDefault(x => x.AccountId == request.AccountId);
            var query_transactions = _context.Transactions.Where(x => x.AccountId == request.AccountId);

            response.Account = new AccountDto()
            {
                AccountId = query_account.AccountId,
                Balance = query_account.Balance,
                Created = query_account.Created,
                Frequency = query_account.Frequency,
                Transactions = query_transactions.Select(t => new TransactionDto()
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date,
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank
                }).ToList()
            };

            //response.Account = Mapper.Map<Account, AccountDto>(query_account);
            //response.Transactions = Mapper.Map<List<Transaction>, List<TransactionDto>>(query_transactions.ToList());

            return response;
        }
    }
}
