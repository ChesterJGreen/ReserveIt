using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Utilities
{
    public static class ModelExtensions
    {
        public static RoomDTO ConvertToResponseDto(this ConferenceRoom room)
        {
            return new RoomDTO(room);
        }
        public static IEnumerable<RoomDTO> ConvertToResponseDto(this IEnumerable<ConferenceRoom> rooms)
        {
            return rooms.Select(thisRoom => new RoomDTO(thisRoom));
        }
        //List<RoomDTO> returnMe = new List<RoomDTO>();
        //foreach (ConferenceRoom thisRoom in rooms)
        // {
        //  var newDto = new RoomDTO()
        //  {
        //  Id = thisRoom.Id,
        //    Description = thisRoom.Description,
        //        Location = thisRoom.Location
        //};
        //ReturnMe.Add(newDto);
        //    return returnMe;
    }
}
