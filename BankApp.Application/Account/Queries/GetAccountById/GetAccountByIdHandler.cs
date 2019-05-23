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

            var query_account = _context.Accounts.SingleOrDefault(x => x.AccountId == request.AccountId);
            //var query_transactions = _context.Transactions.Where(x => x.AccountId == request.AccountId);

            response.Account = new AccountDto()
            {
                AccountId = query_account.AccountId,
                Balance = query_account.Balance,
                Created = query_account.Created,
                Frequency = query_account.Frequency
            };

            //response.Transactions = query_transactions.Select(t => new TransactionDto()
            //{
            //    TransactionId = t.TransactionId,
            //    AccountId = t.AccountId,
            //    Date = t.Date,
            //    Type = t.Type,
            //    Operation = t.Operation,
            //    Amount = t.Amount,
            //    Balance = t.Balance,
            //    Symbol = t.Symbol,
            //    Bank = t.Bank
            //}).ToList();

            //response.TotalTransactions = response.Transactions.Count();
            //response.TotalPages = (response.TotalTransactions / request.Amount);
            
            return response;
        }
    }
}
