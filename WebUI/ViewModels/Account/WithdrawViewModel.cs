using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class WithdrawViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
