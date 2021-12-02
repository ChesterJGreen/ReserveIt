using Microsoft.AspNetCore.Mvc;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserveIt.Managers;
using ReserveIt.Models.Response;
using ReserveIt.Utilities;

namespace ReserveIt.Controllers
{
    [Route("api/conferences")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferencesManager _manager;
        public ConferencesController(IConferencesManager manager)
        {
            _manager = manager;
        }
        [HttpGet("list")]
        public JsonResult GetAllRooms()
        {
            var conferenceRooms = _manager.GetAllRoomsReadOnly();
            return new JsonResult(conferenceRooms);
        }

        [HttpGet("{id}/availability")]
        public JsonResult GetRoomAvailability(int id)
        {
            var room = _manager.GetRoom(id);
            if (room == null)
                return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
            var reservationToReturn = room.ReservationDtos;
            return new JsonResult(reservationToReturn);
        }
        [HttpGet("{id}")]
        public JsonResult GetSingleRoom(int id)
        {
            var room = _manager.GetRoom(id);
            if (room == null) return new JsonResult("Room is either not available or non-existnant") { StatusCode = 404 };
            SingleRoomResponse response = room.ConvertToResponseDto();
            return new JsonResult(response);
        }
        //[HttpPost]
        //public IActionResult CreateConferenceRoom([FromBody] ConferenceRoom newConferenceRoom)
        //{
        //    ConferenceRoom toBeCreated = _manager.CreateRoom(newConferenceRoom);
        //    return Ok(toBeCreated);
        //}
        //[HttpPut("{id}")]
        //public IActionResult EditConferenceRoom([FromBody] ConferenceRoom conferenceRoom)
        //{
        //    var room = _manager.GetRoom(conferenceRoom.Id);
        //    if (room == null)
        //    {
        //        return BadRequest("room not found");
        //    }    
        //    ConferenceRoom toBeEdited = _manager.EditRoom(conferenceRoom);
        //    return Ok(toBeEdited);
        //}
        //[HttpDelete("{id}")]
        //public void DeleteConferenceRoom(int id)
        //{
        //    var room = _manager.GetRoom(id);
        //    if (room == null)
        //        BadRequest("Room Not Found");
        //    ConferenceRoom toBeDeleted = _manager.DeleteRoom(id);
        //}
    }
   


}
