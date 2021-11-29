using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Data
{
    public class ConferencesRepository
    {
        private readonly ResContext _context;
        public ConferencesRepository(ResContext context)
        {
            _context = context;
        }
        public void AddConference(ConferenceRoom conference)
        {
            _context.ConferenceRooms.Add(conference);
        }
        public void RemoveConference(ConferenceRoom conference)
        {
            _context.ConferenceRooms.Remove(conference);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<ConferenceRoom[]> GetConferenceRoomsAsync()
        {
            
            return await _context.ConferenceRooms.Find();
        }


    }
}
