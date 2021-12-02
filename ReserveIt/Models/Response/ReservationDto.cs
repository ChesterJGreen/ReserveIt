using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public class ReservationDto
    {
        public ReservationDto(Reservation reservation)
        {
            Id = reservation.Id;
            StartDateTime = reservation.StartDateTime;
            EndDateTime = reservation.EndDateTime;
            ResourceId = reservation.ResourceId;
            
            
        }
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
