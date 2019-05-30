using BankApp.Application.Queries;
using BankApp.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Queries
{
    public class GetLatestCustomerHandler : IRequestHandler<GetLatestCustomerRequest, GetLatestCustomerResponse>
    {
        private readonly BankContext _context;

        public GetLatestCustomerHandler(BankContext context)
        {
            _context = context;
        }

        public async Task<GetLatestCustomerResponse> Handle(GetLatestCustomerRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Customers.Last();

            return new GetLatestCustomerResponse { CustomerId = query.CustomerId };
        }
    }
}
