using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Utilities.Extensions
{
    public static class ModelExtensions
    {
        public static RoomDTO ConvertToResponseDto(this ConferenceRoom room)
        {
            if (room == null)
                throw new ArgumentNullException("Conference Room", "The value passed for the room parameter is null.");

            return new RoomDTO(room);
        }
        public static RoomDTO ConvertToResponseDto(this ConferenceRoom room, AutoMapper.IMapper mapper)
        {
            if (room == null)
                throw new ArgumentNullException("Conference Room", "The value passed for the room parameter is null.");



            var roomDto = mapper.Map<RoomDTO>(room);
            return roomDto;
        }
        public static IEnumerable<RoomDTO> ConvertToResponseDto(this IEnumerable<ConferenceRoom> rooms)
        {
            if (rooms == null)
                throw new ArgumentNullException("Conference Rooms Collection", "The IEnumberable<ConferenceRoom>is incorrectly set to null.");
            var validRooms = rooms.Where(room => room != null && room.Id != null);
            return validRooms.Select(room => room.ConvertToResponseDto());
        }
        public static IEnumerable<RoomDTO> ConvertToResponseDto(this IEnumerable<ConferenceRoom> rooms, AutoMapper.IMapper mapper)
        {
            if (rooms == null)
                throw new ArgumentNullException("Conference Rooms Collection", "The IEnumberable<ConferenceRoom>is incorrectly set to null.");
            var validRooms = rooms.Where(room => room != null && room.Id != null);
            return validRooms.Select(room => room.ConvertToResponseDto(mapper));
        }
    }
        public static class IdentityExtensions
        {
            /// <summary>
            /// get the user id from the token claims
            /// </summary>
            /// <param name="claimsprincipal"></param>
            /// <returns>NULL if not found; otherwise, the value</returns>
            public static int? GetUserIdFromClaims(this System.Security.Claims.ClaimsPrincipal claimsprincipal)
            {
                var nameClaim = claimsprincipal.Claims.SingleOrDefault(claim => claim.Type == System.Security.Claims.ClaimTypes.Name);

                int userId;
                if (false == Int32.TryParse(nameClaim?.Value, out userId))
                {
                    return null;
                }
                return userId;
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
