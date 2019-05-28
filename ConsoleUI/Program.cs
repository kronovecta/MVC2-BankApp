using BankApp.Data;
using BankApp.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using BankApp.Application;
using BankApp.Application.Queries;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool alive = true;
            //var utils = new Utilities();

            Console.Write("Enter a client name: ");
            var response = Console.ReadLine();

            Console.Write("How many results: ");
            int.TryParse(Console.ReadLine(), out int limit);

            var pagenr = 1;

            while (alive)
            {
                var request = new GetCustomerByNameRequest()
                {
                    Search = response,
                    Limit = limit,  
                    Offset = pagenr
                };

                var query = new GetCustomersByNameHandler().Handler(request);

                foreach (var customer in query.Customers)
                {
                    Console.WriteLine($"[ID: {customer.CustomerId}] Name: {customer.Givenname} {customer.Surname}");
                }

                Console.WriteLine("\nPress enter for more results, type 'exit' to quit");
                var res = Console.ReadLine();

                switch (res)
                {
                    default:
                        //offset += limit;
                        pagenr++;
                        continue;
                    case "exit":
                        alive = false;
                        break;
                }
            }
        }
    }
}