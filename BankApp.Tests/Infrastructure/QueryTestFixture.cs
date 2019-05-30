using AutoMapper;
using BankApp.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankApp.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public BankContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = BankContextFactory.Create();
        }

        public void Dispose()
        {
            BankContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
