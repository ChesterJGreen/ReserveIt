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
        /// <summary>
        /// retrieves all <see cref="ConferenceRoom"/>s
        /// </summary>
        /// <returns>a List of all <see cref="ConferenceRoom"/></returns>
        public List<ConferenceRoom> GetAllRoomsReadOnly();
        /// <summary>
        /// retrieves a <see cref="ConferenceRoom"/> entity by the ID
        /// </summary>
        /// <param name="id">the ID of the room</param>
        /// <returns>a <see cref="ConferenceRoom"/> model representing the room</returns>
        public ConferenceRoom GetRoom(int id);
        /// <summary>
        /// patches a specific <see cref="ConferenceRoom"/>, allowing to change either the Name or Location property
        /// </summary>
        /// <param name="room">The specific conferenceRoom to be patched</param>
        /// <param name="updateRequest">The property to be changed on the conferenceRoom</param>
        /// <returns> A <see cref="ConferenceRoom"/> with the updated changes implemented</returns>
        public ConferenceRoom PatchRoom(ConferenceRoom room, ConferenceRoomUpdateRequest updateRequest);
        /// <summary>
        /// removes a specified <see cref="ConferenceRoom"/>
        /// </summary>
        /// <param name="room">The specific conferenceRoom to be patched</param>
        /// <returns>success or other result</returns>
        public void RemoveRoom(ConferenceRoom room);
        

    }
}
