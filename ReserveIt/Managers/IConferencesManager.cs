using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public interface IConferencesManager
    {
        public List<ConferenceRoom> GetAllRoomsReadOnly();
        public ConferenceRoom GetRoom(int id);
        //public CreateRoom(conferenceRoom);
        //public EditRoom(conferenceRoom);
        //public DeleteRoom(conferenceRoom);

    }
}
