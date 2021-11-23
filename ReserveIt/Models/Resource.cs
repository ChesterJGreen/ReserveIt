using ReserveIt.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public abstract class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ResourceTimeZone ResourceTimeZone { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
