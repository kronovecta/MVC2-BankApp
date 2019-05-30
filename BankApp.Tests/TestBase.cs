using BankApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Tests
{
    public class TestBase
    {
        public BankContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<BankContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new BankContext(builder.Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
