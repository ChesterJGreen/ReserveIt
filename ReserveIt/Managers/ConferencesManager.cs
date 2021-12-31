using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserveIt.Data;
using ReserveIt.Models;
using ReserveIt.Models.Request;
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
        public ConferenceRoom PatchRoom([FromBody] ConferenceRoom original, [FromBody] ConferenceRoomUpdateRequest updateRoomRequest)
        {
            original.Name = updateRoomRequest.Name ??  original.Name;
            original.Location = updateRoomRequest.Location ?? original.Location;
            //I know I'm missing a step in here. I need to send the new object to the data table

           _context.ConferenceRooms.Update(original);
           _context.SaveChangesAsync();
            return original;
        }
        public void RemoveRoom(ConferenceRoom roomToDelete)
        {

            _context.ConferenceRooms.Remove(roomToDelete);
            _context.SaveChangesAsync();
            
        }
    }
}
