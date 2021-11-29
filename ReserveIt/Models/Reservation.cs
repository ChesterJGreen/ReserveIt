using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public virtual ConferenceRoom ConferenceRoom { get; set; }
    }
}
