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
        public static SingleRoomResponse ConvertToResponseDto(this ConferenceRoom room)
        {
            return new SingleRoomResponse(room);
        }
    }
}
