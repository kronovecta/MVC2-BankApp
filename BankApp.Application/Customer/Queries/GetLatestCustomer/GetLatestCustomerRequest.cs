using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public class GetLatestCustomerRequest : IRequest<GetLatestCustomerResponse>
    {
    }
}
