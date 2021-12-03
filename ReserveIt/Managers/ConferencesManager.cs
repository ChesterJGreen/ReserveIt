using Microsoft.EntityFrameworkCore;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public class ConferencesManager : IConferencesManager
    {
        private readonly ResContext _context;
                
        public ConferencesManager(ResContext context)
        {
            _context = context;
            
        }
        public List<ConferenceRoom> GetAllRoomsReadOnly()
        {
            return _context.ConferenceRooms.AsNoTracking().ToList();
        }
        public ConferenceRoom GetRoom(int id)
        {
            return _context.ConferenceRooms.Include(r => r.Reservations).Single(r => r.Id == id);
        }
        //public  ConferenceRoom CreateRoom(ConferenceRoom newConferenceRoom)
        //{
        //    throw new NotImplementedException();
        //}
        //public ConferenceRoom EditRoom(ConferenceRoom conferenceRoom)
        //{
        //    throw new NotImplementedException();
        //}
        //public ConferenceRoom DeleteRoom(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
