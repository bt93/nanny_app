using System;
using System.Collections.Generic;
using System.Text;

namespace NannyApi.Models
{
    public class CareTaker
    {
        public int CareTakerId { get; set; }
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string County { get; set; }
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
