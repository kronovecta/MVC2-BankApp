using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class UpdateCustomerCommand
    {
        public CustomerDto Customer { get; set; }
    }
}
