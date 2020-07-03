using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NannyApi.Models
{
    public class Child
    {
        public int ChildId { get; set; }
        [Required(ErrorMessage = "The 'First Name' field is required.")] 
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The 'Last Name' field is required.")]
        public string LastName { get; set; }
        // Four options: (F)emale, (M)ale, (O)ther, (N)on-Binary
        public char Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "The 'Rate Per Hour' is required")]
        public decimal RatePerHour { get; set; }
        [Required(ErrorMessage = "The 'Needs diapers' field is required.")]
        public bool NeedsDiapers { get; set; }
        public string ImageUrl { get; set; }
        public List<Parent> Parents { get; set; }
    }
}
