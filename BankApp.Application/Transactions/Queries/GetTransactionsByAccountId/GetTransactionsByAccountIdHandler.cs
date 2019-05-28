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
    public class GetTransactionsByAccountIdHandler
    {
        private readonly IBankContext _context;

        public GetTransactionsByAccountIdHandler(IBankContext context)
        {
            _context = context;
        }

        public GetTransactionsByAccountIdResponse Handler(GetTransactionsByAccountIdRequest request)
        {
            var response = new GetTransactionsByAccountIdResponse();

            var query = _context.Transactions.Where(x => x.AccountId == request.AccountId).OrderByDescending(x => x.TransactionId);

            response.Transactions = Mapper.Map<List<Transaction>, List<TransactionDto>>(query.Skip(request.Offset).Take(request.Amount).OrderByDescending(x => x.TransactionId).ToList());
            response.TotalTransactions = query.Count();
            response.TotalPages = response.TotalTransactions / request.Amount;

            return response;
        }
    }
}
