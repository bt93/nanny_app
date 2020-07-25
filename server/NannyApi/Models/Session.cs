using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int ChildId { get; set; }
        [Required(ErrorMessage = "Must have a dropoff time.")]
        public DateTime DropOff { get; set; }
        public DateTime PickUp { get; set; }
        public string Notes { get; set; }
        public Child Child { get; set; }
        public List<Diaper> Diapers { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Nap> Naps { get; set; }
    }
}
