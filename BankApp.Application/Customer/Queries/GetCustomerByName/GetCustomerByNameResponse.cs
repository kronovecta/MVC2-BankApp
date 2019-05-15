﻿using BankApp.Application.DtoObjects;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.GetCustomerByName
{
    public class GetCustomerByNameResponse
    {
        public int TotalCustomerAmount { get; set; }
        public int TotalNumberOfPages { get; set; }
        public IList<CustomerDto> Customers { get; set; }
    }
}