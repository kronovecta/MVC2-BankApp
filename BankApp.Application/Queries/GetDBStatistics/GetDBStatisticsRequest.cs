using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries.GetDBStatistics
{
    public class GetDBStatisticsRequest : IRequest<GetDBStatisticsResponse>
    {
    }
}
