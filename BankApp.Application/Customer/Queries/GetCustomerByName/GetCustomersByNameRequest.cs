using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.GetCustomerByName
{
    public class GetCustomerByNameRequest
    {
        public string Search { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
