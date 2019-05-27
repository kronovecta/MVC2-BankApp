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
    public class TransferTests
    {
        [Fact]
        public void Transfer_InsufficientFunds()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "NegativeAmount")
                .Options;

            using (var context = new BankContext(options))
            {
                var sender = new Account
                {
                    AccountId = 4,
                    Frequency = "Daily",
                    Created = DateTime.Now,
                };
                sender.Deposit(500);

                var reciever = new Account
                {
                    AccountId = 5,
                    Frequency = "Daily",
                    Created = DateTime.Now,
                };

                context.Accounts.AddRange(sender, reciever);
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {
                var command = new TransferCommand()
                {
                    AccountId_Sender = 4,
                    AccountId_Reciever = 5,
                    Amount = 1000
                };

                var handler = new TransferHandler(context);
                Assert.ThrowsAsync<InsufficientFundsException>(() => handler.Handler(command));
            }
        }
    }
}
