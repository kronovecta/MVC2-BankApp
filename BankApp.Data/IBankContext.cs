using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Data
{
    interface IBankContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Disposition> Dispositions { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<PermanentOrder> PermenentOrder { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}
