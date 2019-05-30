using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class TransferViewModel
    {
        #region Sender
        [Required]
        [RegularExpression("[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public int AccountIdSender { get; set; }
        public decimal BalanceSender { get; set; }
        #endregion

        #region Reciever
        [Required]
        [RegularExpression("[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public int AccountIdReciever { get; set; }
        public decimal BalanceReciever { get; set; }
        [Required]
        [RegularExpression("[0-9,\\.]*$", ErrorMessage = "Only numbers allowed")]
        public string Amount { get; set; }
        #endregion
    }
}
