using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Entities;
using BankApp.Application.Queries.GetAccount;
using AutoMapper;
using static BankApp.Application.Queries.GetSingleCustomer.GetCustomerByIdResponse;
using BankApp.Application.DtoObjects;

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

            var query = _context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault(y => y.CustomerId == request.Id);

            response.Customer = Mapper.Map<Customer, CustomerDto>(query);
            response.Customer.Cards = new GetCardByCustomerIdHandler().Handler(new GetCardByCustomerIdRequest() { CustomerId = request.Id }).Cards.ToList();

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
