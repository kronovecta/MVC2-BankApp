using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Queries
{
    public  class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse> 
    {
        public int Id { get; set; }
    }
}
