using BankApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Infrastructure
{
    public class MockDateTime : IDateTime
    {
        private DateTime _date;
        public MockDateTime(string date)
        {
            _date = DateTime.Parse(date);
        }

        public DateTime Now
        {
            get
            {
                if(_date == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return _date;
                }
                
            }
            set
            {
                _date = DateTime.Parse(value.ToString());
            }
        }
    }
}
