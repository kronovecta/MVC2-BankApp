using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using BankApp.Tests.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BankApp.Tests
{
    [Collection("QueryCollection")]
    public class WithdrawTests
    {
        private readonly BankContext _context;

        public WithdrawTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task Withdraw_NegativeAmount()
        {
            var sut = new WithdrawHandler(_context);
            await Assert.ThrowsAsync<NegativeAmountException>(() => sut.Handle(new WithdrawCommand { AccountId = 1, Amount = -50 }, CancellationToken.None));
        }

        [Fact]
        public async Task Withdraw_Overdraft()
        {
            var sut = new WithdrawHandler(_context);
            await Assert.ThrowsAsync<InsufficientFundsException>(() => sut.Handle(new WithdrawCommand { AccountId = 1, Amount = 5000 }, CancellationToken.None));
        }
    }
}
