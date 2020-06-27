using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NannyApi.Models
{
    public class CareTaker
    {
        public int CareTakerId { get; set; }
        public int AddressId { get; set; }
        [Required(ErrorMessage = "The 'First Name field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The 'Last Name field is required.")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        [Required(ErrorMessage = "The 'Email Address' field is required.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "The 'Password' field is required.")]
        public string Password { get; set; }
        [Phone(ErrorMessage = "Please provide a valid phone number")]
        [Required(ErrorMessage = "The 'Phone Number' field is required.")]
        public string PhoneNumber { get; set; }
        public Address Address { get; set; } = new Address();

        public string GetAddress()
        {
            return $"{this.Address.Street} {this.Address.City}, {this.Address.State} {this.Address.Zip} {this.Address.County} {this.Address.Country}";
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
