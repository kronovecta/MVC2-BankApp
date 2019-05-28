using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetTransactionsByAccountIdRequest : IRequest<GetTransactionsByAccountIdResponse>
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
        public int Offset { get; set; }
    }
}
