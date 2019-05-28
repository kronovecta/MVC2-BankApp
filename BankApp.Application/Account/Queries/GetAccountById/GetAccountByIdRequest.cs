using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetAccountByIdRequest : IRequest<GetAccountByIdResponse>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
