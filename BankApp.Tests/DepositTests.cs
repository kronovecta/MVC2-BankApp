using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using BankApp.Tests.Infrastructure;
using MediatR;
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
    public class DepositTests
    {
        private readonly BankContext _context;

        public DepositTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task Deposit_NegativeAmount()
        {
            var sut = new WithdrawHandler(_context);
            await Assert.ThrowsAsync<NegativeAmountException>(() => sut.Handle(new WithdrawCommand { AccountId = 1, Amount = -50 }, CancellationToken.None));
        }
    }
}
