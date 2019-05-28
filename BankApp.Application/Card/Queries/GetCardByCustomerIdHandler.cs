using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BankApp.Application.Queries
{
    class GetCardByCustomerIdHandler
    {
        private readonly BankContext _context;
        public GetCardByCustomerIdHandler(BankContext context)
        {
            _context = context;
        }

        public GetCardByCustomerIdResponse Handler(GetCardByCustomerIdRequest request)
        {
            var response = new GetCardByCustomerIdResponse();

            var query = (from ca in _context.Cards
                         join di in _context.Dispositions on ca.DispositionId equals di.DispositionId
                         join cu in _context.Customers on di.CustomerId equals cu.CustomerId
                         where cu.CustomerId == request.CustomerId select ca);

            response.Cards = query.ProjectTo<CardDto>().ToList();

            if (response != null)
            {
                return response;
            } else
            {
                return null;
            }
            
        }
    }
}
