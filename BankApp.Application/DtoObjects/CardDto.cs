using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.DtoObjects
{
    public class CardDto
    {
        public int CardId { get; set; }
        public int DispositionId { get; set; }
        public string Type { get; set; }
        public DateTime Issued { get; set; }
        public string Cctype { get; set; }
        public string Ccnumber { get; set; }
        public string Cvv2 { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }
    }
}
