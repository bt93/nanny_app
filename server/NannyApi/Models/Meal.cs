using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public int SessionId { get; set; }
        [Required(ErrorMessage = "Must submit a time")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Must have a meal type.")]
        [StringLength(maximumLength: 20,ErrorMessage = "Must be no longer than 20 characters.")]
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
