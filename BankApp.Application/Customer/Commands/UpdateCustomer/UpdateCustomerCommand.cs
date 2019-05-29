using BankApp.Application.DtoObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankApp.Application.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string Givenname { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Adress is required")]
        public string Streetaddress { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Country code is required")]
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
    }
}
