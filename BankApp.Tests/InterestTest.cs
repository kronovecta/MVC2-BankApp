using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Common;
using BankApp.Data;
using BankApp.Domain.Entities;
using BankApp.Infrastructure;
using BankApp.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BankApp.Tests
{
    [Collection("QueryCollection")]
    public class InterestTest
    {
        private readonly BankContext _context;
        private readonly IDateTime _dateTime;

        public InterestTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _dateTime = new MockDateTime("2018-05-30");
        }

        [Fact]
        public async Task Interest_ApplyTest()
        {
            var sut = new ApplyInterestHandler(_context, _dateTime);
            var result = await sut.Handle(new ApplyInterestCommand { AccountId = 3, Amount = 1, PreviousApplication = DateTime.Parse("2017-05-30") }, CancellationToken.None);

            var balance = _context.Accounts.SingleAsync(x => x.AccountId == 3).Result.Balance;

            Assert.Equal(1010.0492M, Math.Round(balance, 4), 4);
        }
    }
}
