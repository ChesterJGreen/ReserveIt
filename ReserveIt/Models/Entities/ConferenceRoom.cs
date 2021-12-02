using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;
using static ReserveIt.Data.Reference;

namespace ReserveIt.Models
{
    public class ConferenceRoom : BaseResource
    {
        public ConferenceRoom()
        {
            Reservations = new HashSet<Reservation>();
        }
        [MaxLength(50)]
        public string Location { get; set; }
        public string BuildingName { get; set; }
        public int SeatingProvided { get; set; }
        [NotMapped]
        public IEnumerable<AssistedLectureDevices> AvailableLectureDevices { get; set; }
        public ResourceType ResourceType { get; set; } = ResourceType.ConferenceRoom;
        [ForeignKey("ResourceId")]
        public virtual IEnumerable<Reservation> Reservations { get; set; }
        





    }
}
