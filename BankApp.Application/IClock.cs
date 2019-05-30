using System;

namespace BankApp.Application
{
    interface IClock
    {
        DateTime Now { get; }
    }
}
