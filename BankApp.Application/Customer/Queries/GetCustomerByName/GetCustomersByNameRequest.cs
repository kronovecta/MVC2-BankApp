﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetCustomerByNameRequest : IRequest<GetCustomerByNameResponse>
    {
        public string Search { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
