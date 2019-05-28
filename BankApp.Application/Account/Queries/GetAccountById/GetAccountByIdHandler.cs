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

        public GetAccountByIdHandler(BankContext context)
        {
            _context = context;
        }

        public GetAccountByIdResponse Handler(GetAccountByIdRequest request)
        {
            var response = new GetAccountByIdResponse();

            var query_account = _context.Accounts.SingleOrDefault(x => x.AccountId == request.AccountId);

            response.Account = new AccountDto()
            {
                AccountId = query_account.AccountId,
                Balance = query_account.Balance,
                Created = query_account.Created,
                Frequency = query_account.Frequency
            };
            
            return response;
        }
    }
}
