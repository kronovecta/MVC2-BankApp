using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.DtoObjects
{
    class DispositionDto
    {
        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
    }
}
