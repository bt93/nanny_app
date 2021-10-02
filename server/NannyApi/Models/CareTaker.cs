using System.ComponentModel.DataAnnotations;

namespace NannyApi.Models
{
    public class CareTaker
    {
        public int CareTakerId { get; set; }
        public int AddressId { get; set; }
        [Required(ErrorMessage = "The 'First Name' field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The 'Last Name' field is required.")]
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
        public string Salt { get; internal set; }
        public string Token { get; internal set; }
    }

    public class CareTakerSettings
    {
        public int CareTakerId { get; set; }
        public int AddressId { get; set; }
        [Required(ErrorMessage = "The 'First Name' field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The 'Last Name' field is required.")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        [Required(ErrorMessage = "The 'Email Address' field is required.")]
        public string EmailAddress { get; set; }
        [Phone(ErrorMessage = "Please provide a valid phone number")]
        [Required(ErrorMessage = "The 'Phone Number' field is required.")]
        public string PhoneNumber { get; set; }
        public Address Address { get; set; } = new Address();
    }

    /// <summary>
    /// Model to return upon successful login
    /// </summary>
    public class ReturnCareTaker
    {
        public int CareTakerId { get; set; }
        public string EmailAddress { get; set; }
        //public string Password { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; }
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginCareTaker
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
