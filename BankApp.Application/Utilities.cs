using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankApp.Data;

namespace BankApp.Application
{
    public class Utilities
    {
        private static List<Customer> AllCustomers;
        private readonly BankAppContext _context;
        private static int pageNr = 0;

        public Utilities()
        {
            _context = new BankAppContext();
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        public int GetCountofCustomers()
        {
            var amount = _context.Customers.Count();
            return amount;
        }

        public (int total, IEnumerable<Customer> customers) GetCustomersNamed(string name, int limit)
        {
            //if(AllCustomers == null)
            //{
            //    AllCustomers = _context.Customers.AsNoTracking().Where(x => x.Givenname.StartsWith(name)).ToList();
            //}

            var allCustomers = _context.Customers.AsNoTracking().Where(x => x.Givenname.StartsWith(name)).Include(x => x.Dispositions).ThenInclude(y => y.Account).ToList();
            var selectCustomers = allCustomers.Skip(pageNr * limit).Take(limit).ToList();

            pageNr++;

            //switch (limit)
            //{
            //    case 0:
            //        selectCustomers = AllCustomers.ToList();
            //        break;
            //    default:
            //        pageNr++;
            //        //selectCustomers = AllCustomers.Skip(limit * pageNr).Take(limit).ToList();
            //        selectCustomers = _context.Customers.AsNoTracking().Where(x => x.Givenname.StartsWith(name)).Skip(pageNr).Take(limit).ToList();
            //        break;
            //}

            return (total: allCustomers.Count(), customers: selectCustomers);
        }
    }
}
