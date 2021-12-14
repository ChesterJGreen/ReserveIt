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
            if (room == null)
                throw new ArgumentNullException("Conference Room", "The value passed for the room parameter is null.");
            if (room.Id == null || room.Name == null || room.Location == null)
                throw new ArgumentNullException("Conference Room", "A property of the room is incorrectly set to null.");
            return new RoomDTO(room);
        }
        public static IEnumerable<RoomDTO> ConvertToResponseDto(this IEnumerable<ConferenceRoom> rooms)
        {
            if (rooms == null)
                throw new ArgumentNullException("Conference Rooms Collection", "The IEnumberable<ConferenceRoom>is incorrectly set to null.");
            var validRooms = rooms.Where(room => room != null && room.Id != null);
            return validRooms.Select(room => room.ConvertToResponseDto());
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
