using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.Models
{
    public class Allergy
    {
        public int AllergyId { get; set; }
        public int AllergyTypeId { get; set; }
        public string Name { get; set; }
        public string AllergyType { get; set; }
    }

    public class AllergyType
    {
        public int AllergyTypeId { get; set; }
        public string Name { get; set; }
    }
}
