using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Identity
{
    public class GetRolesRequest : IRequest<GetRolesResponse>
    {
    }
}
