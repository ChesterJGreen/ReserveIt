using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;

namespace ReserveIt.Models
{
    public class ConfereneceRoom
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public VenueTimeZone TimeZone{ get; set; }
        public List<TimeSlot> TimeSlots { get; set; }

        public class TimeSlot { 
            public string Name { get; set; }
            public int Time { get; set; }
        }
        
    }
}
