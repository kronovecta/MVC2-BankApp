using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetCustomerByNamesCityRequest
    {
        public string Search { get; set; }
        public string City { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
