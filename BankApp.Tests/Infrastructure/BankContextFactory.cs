using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Tests.Infrastructure
{
    class BankContextFactory
    {
        public static BankContext Create()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BankContext(options);

            context.Database.EnsureCreated();

            context.Accounts.AddRange(new[]
            {
                new Account { AccountId = 1, Balance = 0 },
                new Account { AccountId = 2, Balance = 50 },
                new Account { AccountId = 3, Balance = 1000, Frequency = "weekly" }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(BankContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
