using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Application.Card.Queries
{
    public class GetCardByCustomerIdResponse
    {
        public class CardDto
        {
            public int CardId { get; set; }
            public string Type { get; set; }
            public DateTime Issued { get; set; }
            public string CCType { get; set; }
            public string CCNumber { get; set; }
            public string CVV2 { get; set; }
            public int ExpiryMonth { get; set; }
            public int ExpiryYear { get; set; }
        }

        public IList<CardDto> Cards { get; set; }
    }
}
