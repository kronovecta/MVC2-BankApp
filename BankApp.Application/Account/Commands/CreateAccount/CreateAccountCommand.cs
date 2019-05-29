using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class CreateAccountCommand : IRequest
    {
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
