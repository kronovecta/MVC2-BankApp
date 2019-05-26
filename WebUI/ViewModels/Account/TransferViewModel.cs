using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class TransferViewModel
    {
        #region Sender
        public int AccountIdSender { get; set; }
        public decimal BalanceSender { get; set; }
        #endregion

        #region Reciever
        public int AccountIdReciever { get; set; }
        public decimal BalanceReciever { get; set; }
        public decimal Amount { get; set; }
        #endregion
    }
}
