using ReserveIt.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public abstract class Resource
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public VenueTimeZone VenueTimeZone { get; set; }
    }
}
