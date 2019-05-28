using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class InterestViewModel
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime PreviousApplication { get; set; }
    }
}
