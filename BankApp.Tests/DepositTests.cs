using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BankApp.Tests
{
    public class DepositTests
    {
        private readonly IMediator _mediator;
        private readonly BankContext _context;

        public DepositTests(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Fact]
        public void Deposit_NegativeAmount()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "NegativeAmount")
                .Options;

            using (var context = new BankContext(options))
            {
                var account = new Account
                {
                    AccountId = 3,
                    Balance = 0
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {
                var sut = new DepositHandler(context);

                Assert.ThrowsAsync<NegativeAmountException>(() => sut.Handle(new DepositCommand { AccountId = 3, Amount = -50 }, CancellationToken.None));
            }

            //using (var context = new BankContext(options))
            //{
            //    var command = new WithdrawCommand() { AccountId = 3, Amount = -50 };
            //    var handler = new WithdrawHandler(context);

            //    //Assert.ThrowsAsync<NegativeAmountException>(() => handler.Handler(command));
            //    Assert.ThrowsAsync<NegativeAmountException>(() => _mediator.Send(command));
            //}
        }
    }
}
