using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries.GetDBStatistics
{
    public class GetDBStatisticsResponse
    {
        public int Customers { get; set; }
        public int Accounts  { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
