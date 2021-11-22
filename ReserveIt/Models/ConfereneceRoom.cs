using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;

namespace ReserveIt.Models
{
    public class ConfereneceRoom : Resource
    {
        public string Name { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }

        public class TimeSlot { 
            public string Name { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
        }
        
    }
}
