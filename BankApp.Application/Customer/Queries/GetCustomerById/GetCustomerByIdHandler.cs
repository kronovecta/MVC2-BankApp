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
            //Mapper.Initialize(cfg =>
            //{
            //    //cfg.CreateMap<Customer, CustomerDto>();
            //    cfg.AddProfile(new AutoMapperProfile());
            //});

            var response = new GetCustomerByIdResponse();

            var query = _context.Customers.Include(x => x.Dispositions).ThenInclude(y => y.Cards).OrderBy(x => x.CustomerId).SingleOrDefault(y => y.CustomerId == request.Id);

            //response.Customer = query
            //    .AsNoTracking()
            //    .Select(c => new GetCustomerByIdResponse.CustomerDto()
            //    {
            //        Id = c.CustomerId,
            //        FirstName = c.Givenname,
            //        LastName = c.Surname,
            //        City = c.City,
            //        Cards = 
            //        //Cards = new GetCardByCustomerIdHandler().Handler(new GetCardByCustomerIdRequest() { CustomerId = request.Id }).Cards,
            //        //Accounts = new GetAccountHandler().Handler(new GetAccountRequest() { CustomerId = request.Id }).Accounts

            //    }).SingleOrDefault();

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
