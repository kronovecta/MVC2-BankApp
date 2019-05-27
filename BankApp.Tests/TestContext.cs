using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Tests
{
    class TestContext : BankContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseInMemoryDatabase();
        }
    }
}
