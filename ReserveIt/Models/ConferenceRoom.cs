using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Enums;

namespace ReserveIt.Models
{
    public class ConferenceRoom : Resource
    {
        public string BuildingName { get; set; }
        public int SeatingProvided { get; set; }
        public List<AssisstedLectureDevices> AvailableLectureDevices { get; set; }
        





    }
}
