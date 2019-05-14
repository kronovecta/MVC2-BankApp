using BankApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.Application.Card.Queries
{
    class GetCardByCustomerIdHandler
    {
        private readonly BankContext _context;
        public GetCardByCustomerIdHandler()
        {
            _context = new BankContext();
        }

        public GetCardByCustomerIdResponse Handler(GetCardByCustomerIdRequest request)
        {
            var response = new GetCardByCustomerIdResponse();

            var query = (from ca in _context.Cards
                         join di in _context.Dispositions on ca.DispositionId equals di.DispositionId
                         join cu in _context.Customers on di.CustomerId equals cu.CustomerId
                         where cu.CustomerId == request.CustomerId select ca);

            response.Cards = query
                .AsNoTracking()
                .Select(c => new GetCardByCustomerIdResponse.CardDto()
                {
                    CardId = c.CardId,
                    Type = c.Type,
                    Issued = c.Issued,
                    CCType = c.Cctype,
                    CCNumber = c.Ccnumber,
                    CVV2 = c.Cvv2,
                    ExpiryMonth = c.ExpM,
                    ExpiryYear = c.ExpY
                })
                .OrderBy(x => x.CardId)
                .ToList();

            if(response != null)
            {
                return response;
            } else
            {
                return null;
            }
            
        }
    }
}
