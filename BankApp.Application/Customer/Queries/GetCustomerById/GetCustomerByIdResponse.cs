using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Application.DtoObjects;

namespace BankApp.Application.Queries
{
    public class GetCustomerByIdResponse
    {
        public CustomerDto Customer { get; set; }
    }
}
