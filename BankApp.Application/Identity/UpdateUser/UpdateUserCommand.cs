using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
