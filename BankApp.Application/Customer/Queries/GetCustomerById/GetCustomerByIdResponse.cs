using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Application.Card.Queries;

namespace BankApp.Application.Queries.GetSingleCustomer
{
    public class GetCustomerByIdResponse
    {
        public class CustomerDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public Decimal TotalBalance { get; set; }

            public IList<CardDto> Cards { get; set; }
            public IEnumerable<Account> Account { get; set; }

            //public IEnumerable<Disposition> Dispositions { get; set; }
        }

        public CustomerDto Customer { get; set; }
    }
}
