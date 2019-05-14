using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.GetCustomerByName
{
    public class GetCustomerByNameResponse
    {
        public class CustomerDto
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string City { get; set; }
            public int NumberOfAccounts { get; set; }
        }

        public int TotalCustomerAmount { get; set; }
        public int TotalNumberOfPages { get; set; }
        public IList<CustomerDto> Customers { get; set; }
    }
}
