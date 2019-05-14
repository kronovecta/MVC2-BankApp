using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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

            public IEnumerable<Card> Cards { get; set; }
            public IEnumerable<Account> Account { get; set; }

            public IEnumerable<Disposition> Dispositions { get; set; }
        }

        public CustomerDto Customer { get; set; }
    }
}
