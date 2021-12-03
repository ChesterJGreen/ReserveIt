﻿namespace ReserveIt.Models.Response
{
    public class RoomDTO
    {
        public RoomDTO(ConferenceRoom resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Location = resource.Location;
                   
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        

    }
       
}