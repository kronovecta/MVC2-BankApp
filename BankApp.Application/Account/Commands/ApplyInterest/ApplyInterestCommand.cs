using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class ApplyInterestCommand : IRequest
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime PreviousApplication { get; set; }
    }
}
