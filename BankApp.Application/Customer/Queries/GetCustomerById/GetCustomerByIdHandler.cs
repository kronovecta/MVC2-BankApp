using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Entities;
using BankApp.Application.Card.Queries;

namespace BankApp.Application.Queries.GetSingleCustomer
{
    public class GetCustomerByIdHandler
    {
        private readonly BankContext _context;

        public GetCustomerByIdHandler()
        {
            _context = new BankContext();
        }

        public GetCustomerByIdResponse Handler(GetCustomerByIdRequest request)
        {
            var response = new GetCustomerByIdResponse();

            var query = _context.Customers.OrderBy(x => x.CustomerId).Where(y => y.CustomerId == request.Id);

            response.Customer = query.AsNoTracking().Select(c => new GetCustomerByIdResponse.CustomerDto()
            {
                Id = c.CustomerId,
                FirstName = c.Givenname,
                LastName = c.Surname,
                City = c.City,
                //Cards = new GetCardByCustomerIdHandler().Handler(new GetCardByCustomerIdRequest() { CustomerId = request.Id })
                
            }).SingleOrDefault();

            

            //var query2 = query
            //    .AsNoTracking()
            //    .Where(c => c.CustomerId == request.Id)

            //    //.Include(d => d.Dispositions)
                
            //    .Include(d => d.Dispositions)
            //        .ThenInclude(a => a.Account)
            //            .ThenInclude(l => l.Loans)

            //    .Include(d => d.Dispositions)
            //        .ThenInclude(a => a.Account)
            //            .ThenInclude(po => po.PermenentOrder)

            //    .Include(d => d.Dispositions)
            //        .ThenInclude(c => c.Cards)

            //    .SingleOrDefault(x => x.CustomerId.Equals(request.Id));

            //response.Customer = new GetCustomerByIdResponse.CustomerDto()
            //{
                //Id = query.CustomerId,
                //FirstName = query.Givenname,
                //LastName = query.Surname,
                //City = query.City,
                ////Dispositions = query.Dispositions,
                ////Cards = query.Dispositions.Select(x => x.Cards).SingleOrDefault(),
                //Account = query.Dispositions.Select(y => y.Account),
                //TotalBalance = query.Dispositions.Sum(x => x.Account.Balance)
            //};

                Cards = query.Dispositions.Select(c => new GetCustomerByIdResponse.CustomerCardDto()
                {
                    CardId = c.Cards
                })
            };

            if(response.Customer.Cards.Count() > 0)
            {
                foreach (var card in response.Customer.Cards) { card.Disposition = null; }
            }

            if (response.Customer.Account.Count() > 0)
            {
                foreach (var account in response.Customer.Account) { account.Dispositions = null; }
            }

            if (response != null)
            {
                return response;
            } else
            {
                throw new NullReferenceException();
            }
            
        }
    }
}
