using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;

namespace ReserveIt.Models
{
    public class ConferenceRoom : Resource
    {
        public string BuildingName { get; set; }
        public int SeatingProvided { get; set; }
        [NotMapped]
        public IEnumerable<AssisstedLectureDevices> AvailableLectureDevices { get; set; }
        





    }
}
