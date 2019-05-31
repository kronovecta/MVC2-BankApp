using BankApp.Application.DtoObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels
{
    public class SearchCustomerViewModel
    {
        //Input
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public int Amount { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalPages { get; set; }
        public int PageNr { get; set; }
        public int CustomerId { get; set; }

        //Output
        public IList<CustomerDto> Customers { get; set; }

        public SearchCustomerViewModel()
        {
            Customers = new List<CustomerDto>();
        }
    }
}
