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
        [EmailAddress]
        [Required(ErrorMessage = "The 'Email Address' field is required.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "The 'Password' field is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The 'Phone Number' field is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The 'Street' field is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "The 'City' field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "The 'State' field is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "The 'Zip Code' field is required.")]
        public int Zip { get; set; }
        public string County { get; set; }
        [Required(ErrorMessage = "The 'Country' field is required.")]
        public string Country { get; set; }

        public string GetAddress()
        {
            return $"{this.Street} {this.City}, {this.State} {this.Zip} {this.County} {this.Country}";
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
