using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Identity
{
    public class GetUserByIdCommand : IRequest<GetUserByIdResponse>
    {
        public string Id { get; set; }
    }
}
