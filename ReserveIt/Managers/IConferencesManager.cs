using ReserveIt.Models;
using ReserveIt.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public interface IConferencesManager
    {
        public List<ConferenceRoom> GetAllRoomsReadOnly();
        /// <summary>
        /// retrieves a room entity by the ID
        /// </summary>
        /// <param name="id">the ID of the room</param>
        /// <returns>a <see cref="ConferenceRoom"/> model representing the room</returns>
        public ConferenceRoom GetRoom(int id);
        public ConferenceRoom PatchRoom(ConferenceRoom room, ConferenceRoomUpdateRequest updateRequest);
        

    }
}
