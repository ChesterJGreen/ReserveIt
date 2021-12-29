using System.Collections.Generic;

namespace ReserveIt.Models.Response
{
    public class RoomDTO
    {
        public RoomDTO()
        {
        }

        public RoomDTO(ConferenceRoom resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Location = resource.Location;
                   
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; } = string.Empty;
        
        public IEnumerable<string> AvailableLectureDevices { get; set; }
    }
       
}
