using BankApp.Application.Commands;
using BankApp.Application.Exceptions;
using BankApp.Data;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankApp.Tests
{
    public class DepositTests
    {
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
                    Frequency = "Daily"
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {
                var command = new WithdrawCommand() { AccountId = 3, Amount = -50 };
                var handler = new WithdrawHandler(context);

                Assert.ThrowsAsync<NegativeAmountException>(() => handler.Handler(command));
            }
        }
    }
}
