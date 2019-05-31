﻿using AutoMapper;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Application.DtoObjects;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdRequest, GetAccountByIdResponse>
    {
        private readonly IBankContext _context;

        public GetAccountByIdHandler(IBankContext context)
        {
            _context = context;
        }

        public Task<GetAccountByIdResponse> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
