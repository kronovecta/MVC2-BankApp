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

            //var command = new ApplyInterestCommand { AccountId = 7, Amount = 1, PreviousApplication = DateTime.Parse("2018-05-30")};
            //var handler = new ApplyInterestHandler(context);
            //handler.Handler(command);

            //var account = context.Accounts.SingleAsync(x => x.AccountId == 7).Result;

            //Assert.Equal(1010.0492M, Math.Round(account.Balance, 4), 4);

            var sut = new ApplyInterestHandler(_context, _dateTime);
            var result = await sut.Handle(new ApplyInterestCommand { AccountId = 3, Amount = 1, PreviousApplication = DateTime.Parse("2017-05-30") }, CancellationToken.None);

            var balance = _context.Accounts.SingleAsync(x => x.AccountId == 3).Result.Balance;

            Assert.Equal(1010.0492M, Math.Round(balance, 4), 4);

            //await Assert.ThrowsAsync<InsufficientFundsException>(() => sut.Handle(new ApplyInterestCommand { AccountId_Reciever = 1, AccountId_Sender = 2, Amount = 50 }, CancellationToken.None));
        }
    }
}
