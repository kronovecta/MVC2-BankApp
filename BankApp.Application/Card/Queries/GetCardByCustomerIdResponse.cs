using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetCardByCustomerIdResponse
    {
        public List<CardDto> Cards { get; set; }
    }
}
