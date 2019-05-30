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
    public class TransferTests
    {
        private readonly BankContext _context;

        public TransferTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task Transfer_InsufficientFunds()
        {
            var sut = new TransferHandler(_context);
            var result = await sut.Handle(new TransferCommand { AccountId_Reciever = 1, AccountId_Sender = 2, Amount = 50 }, CancellationToken.None);

            await Assert.ThrowsAsync<InsufficientFundsException>(() => sut.Handle(new TransferCommand { AccountId_Reciever = 1, AccountId_Sender = 2, Amount = 50 }, CancellationToken.None));
        }
    }
}
