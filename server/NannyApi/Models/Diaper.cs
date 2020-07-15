using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.Models
{
    public class Diaper
    {
        public int DiaperId { get; set; }
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string Notes { get; set; }
    }
}
