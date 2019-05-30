using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace BankApp.Tests
{
    public class WithdrawTests
    {
        private readonly IMediator _mediator;

        public WithdrawTests(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Fact]
        public void Withdraw_NegativeAmount()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "NegativeAmount")
                .Options;

            using(var context = new BankContext(options))
            {
                var command = new WithdrawCommand() { AccountId = 1, Amount = -50 };
                //var handler = new WithdrawHandler(context);

                //Assert.ThrowsAsync<NegativeAmountException>(() => handler.Handler(command));
                Assert.ThrowsAsync<NegativeAmountException>(() => _mediator.Send(command));
            }
        }

        [Fact]
        public void Withdraw_Overdraft()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "NegativeAmount")
                .Options;

            using (var context = new BankContext(options))
            {
                var account = new Account
                {
                    AccountId = 2,
                    Frequency = "Daily",
                    Balance = 500
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {
                var command = new WithdrawCommand { AccountId = 2, Amount = 1000 };
                //var handler = new WithdrawHandler(context);

                //Assert.ThrowsAsync<InsufficientFundsException>(() => handler.Handler(command));
                Assert.ThrowsAsync<InsufficientFundsException>(() => _mediator.Send(command));
            }

        }
    }
}
