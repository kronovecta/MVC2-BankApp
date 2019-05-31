using BankApp.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Queries.GetDBStatistics
{
    public class GetDBStatisticsHandler : IRequestHandler<GetDBStatisticsRequest, GetDBStatisticsResponse>
    {
        private readonly IBankContext _context;
        public GetDBStatisticsHandler(IBankContext context)
        {
            _context = context;
        }

        public async Task<GetDBStatisticsResponse> Handle(GetDBStatisticsRequest request, CancellationToken cancellationToken)
        {
            var customers = _context.Customers.Count();
            var accounts = _context.Accounts.Count();
            var totalBalance = _context.Accounts.Sum(x => x.Balance);

            //return response;
            return new GetDBStatisticsResponse
            {
                Customers = customers,
                Accounts = accounts,
                TotalBalance = totalBalance
            };
        }
    }
}
