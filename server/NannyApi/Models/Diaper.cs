using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.Models
{
    public class Diaper
    {
        public int DiaperId { get; set; }
        public int SessionId { get; set; }
        [Required(ErrorMessage = "Must submit a time.")]
        public DateTime Time { get; set; }
        public string Notes { get; set; }
    }
}
