using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class WithdrawCommand : IRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
