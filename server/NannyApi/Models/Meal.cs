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
        public DateTime Time { get; set; }
        [StringLength(20, ErrorMessage = "Must be length of 20 characters.")]
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
