using Microsoft.AspNetCore.Mvc;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Managers;
using Microsoft.EntityFrameworkCore;

namespace ReserveIt.Controllers
{
    [Route("api/conferences")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly ConferencesManager _manager;
        public ConferencesController(ConferencesManager manager)
        {
            _manager = manager;
        }
        [HttpGet("list")]
        public List<ConferenceRoom> GetAllRooms()
        {
            var conferenceRooms = _manager.GetAllRoomsReadOnly();
            return conferenceRooms;
        }

        [HttpGet("{id}/availability")]
        public IActionResult GetRoomAvailability(int id)
        {
            var room = _manager.GetRoom(id);
            var roomInQuestion = room.Find(x => x.Id == id);
            if (roomInQuestion == null)
                return BadRequest("Room is either not available or non-existant") ;
            var reservationToReturn = roomInQuestion.Reservations;
            return Ok(reservationToReturn);
        }
        [HttpPost]
        public IActionResult CreateConferenceRoom([FromBody] ConferenceRoom newConferenceRoom)
        {
            ConferenceRoom toBeCreated = _manager.CreateRoom(newConferenceRoom);
            return Ok(toBeCreated);
        }
        [HttpPut("{id}")]
        public IActionResult EditConferenceRoom([FromBody] int id, ConferenceRoom conferenceRoom)
        {
            ConferenceRoom toBeEdited = _manager.EditRoom(id, conferenceRoom);
            return Ok(toBeEdited);
        }
        [HttpDelete("{id}")]
        public void DeleteConferenceRoom(int id)
        {
            ConferenceRoom toBeDeleted = _manager.DeleteRoom(id);
        }
    }

}
