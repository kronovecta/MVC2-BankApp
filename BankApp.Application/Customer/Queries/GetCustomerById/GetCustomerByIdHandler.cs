using AutoMapper;
using BankApp.Application.DtoObjects;
using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BankApp.Application.Queries
{
    public class GetCustomerByIdHandler
    {
        private readonly BankContext _context;

        public GetCustomerByIdHandler(BankContext context)
        {
            _context = context;
        }

        public GetCustomerByIdResponse Handler(GetCustomerByIdRequest request)
        {
            var response = new GetCustomerByIdResponse();

            var query = _context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault(y => y.CustomerId == request.Id);


            response.Customer = Mapper.Map<Customer, CustomerDto>(query);

            //var cards = new GetCardByCustomerIdHandler().Handler(new GetCardByCustomerIdRequest() { CustomerId = request.Id }).Cards;
            //var accounts = new GetAccountsByUserIdHandler().Handler(new GetAccountsByUserIdRequest() { CustomerId = request.Id }).Accounts;

            //if (cards.Count > 0)
            //{
            //    response.Customer.Cards = cards;
            //}

            //if(accounts != null)
            //{
            //    response.Customer.Accounts = accounts;
            //    response.Customer.TotalBalance = response.Customer.Accounts.Sum(x => x.Balance);
            //}            

            // REPLACE WITH NEW HANDLERS

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
