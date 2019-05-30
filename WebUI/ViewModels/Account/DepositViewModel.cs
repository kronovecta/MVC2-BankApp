using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Account
{
    public class DepositViewModel
    {
        [Required]
        [RegularExpression("[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public int AccountId { get; set; }

        [Required]
        [RegularExpression("[0-9,\\.]*$", ErrorMessage = "Only numbers allowed")]
        public string Amount { get; set; }
    }
}
