using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.Application.Queries.GetDBStatistics
{
    public class GetDBStatisticsHandler
    {
        private readonly BankContext _context;
        public GetDBStatisticsHandler()
        {
            _context = new BankContext();
        }

        public GetDBStatisticsResponse Handler()
        {
            var response = new GetDBStatisticsResponse();

            response.Customers = _context.Customers.Count();
            response.Accounts = _context.Accounts.Count();
            response.TotalBalance = _context.Accounts.Sum(x => x.Balance);

            return response;
        }
    }
}
