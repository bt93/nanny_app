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

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
