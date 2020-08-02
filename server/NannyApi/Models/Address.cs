using System.ComponentModel.DataAnnotations;

namespace NannyApi.Models
{
    public class Address
    {
        [Required(ErrorMessage = "The 'Street' field is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "The 'City' field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "The 'State' field is required.")]
        public string State { get; set; }
        public int Zip { get; set; }
        public string County { get; set; }
        [Required(ErrorMessage = "The 'Country' field is required.")]
        public string Country { get; set; }
    }
}
