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

        public GetCustomerByIdHandler()
        {
            _context = new BankContext();
        }

        public GetCustomerByIdResponse Handler(GetCustomerByIdRequest request)
        {
            var response = new GetCustomerByIdResponse();

            var query = _context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault(y => y.CustomerId == request.Id);

            response.Customer = Mapper.Map<Customer, CustomerDto>(query);
            response.Customer.Cards = new GetCardByCustomerIdHandler().Handler(new GetCardByCustomerIdRequest() { CustomerId = request.Id }).Cards.ToList();
            response.Customer.Accounts = new GetAccountsByUserIdHandler().Handler(new GetAccountsByUserIdRequest() { CustomerId = request.Id }).Accounts;
            response.Customer.TotalBalance = response.Customer.Accounts.Sum(x => x.Balance);

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
