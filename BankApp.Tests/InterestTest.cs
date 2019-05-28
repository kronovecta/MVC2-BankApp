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
    interface IDateTime
    {
        System.DateTime Now { get; }
        void SetTime(string date);
    }

    class DateTime : IDateTime
    {
        private System.DateTime _date;

        public void SetTime(string date)
        {
            _date = System.DateTime.Parse(date);
        }

        public System.DateTime Parse(string date)
        {
            return System.DateTime.Parse(date);
        }


        public System.DateTime Now
        {
            get { return _date; }
        }
    }

    public class InterestTest
    {
        [Fact]
        public void Interest_ApplyTest()
        {
            var date = new DateTime();
            date.SetTime("2018-05-28");

            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "NegativeAmount")
                .Options;

            using (var context = new BankContext(options))
            {
                var account = new Account
                {
                    AccountId = 7,
                    Frequency = "Weekly",
                    Balance = 1000
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            using (var context = new BankContext(options))
            {
                var command = new ApplyInterestCommand { AccountId = 7, Amount = 1, PreviousApplication = date.Parse("2018-05-28") };
                var handler = new ApplyInterestHandler(context);
                handler.Handler(command);

                var account = context.Accounts.SingleAsync(x => x.AccountId == 7).Result;

                Assert.Equal(1010.0492M, Math.Round(account.Balance, 4), 4);
            }
        }
    }
}
