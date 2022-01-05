using Microsoft.AspNetCore.Mvc;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserveIt.Managers;
using ReserveIt.Models.Response;
using ReserveIt.Utilities;
using AutoMapper;
using ReserveIt.Models.Request;

namespace ReserveIt.Controllers
{
    [Route("api/conferences")]
    [ApiController]
    [Produces("application/json")]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferencesManager _manager;
        
        public ConferencesController(Config.BaseControllerDependencies dependencies, IConferencesManager manager) :base(dependencies)
        {
            _manager = manager;
           
        }


        /// <summary>
        /// gets all rooms                                               
        /// </summary>
        /// <returns>an array of all conferenceRooms</returns>
        /// <response code="200">successfully retrieved all rooms</response>
        [HttpGet("list")]
        public JsonResult GetAllRooms()
        {
            var conferenceRooms = Mapper.Map<IEnumerable<RoomDTO>>(_manager.GetAllRoomsReadOnly());
            return new JsonResult(conferenceRooms);
        }
        /// <summary>
        /// gets all availability of a single room
        /// </summary>
        /// <param name="id">the room ID from the URL</param>
        /// <returns>if available,returns the reservations for a specific room</returns>
        [HttpGet("{id}/availability")]
        public JsonResult GetRoomAvailability(int id)
        {
            var room = _manager.GetRoom(id);
            if (room == null)
                return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
            var reservationToReturn = room.ReservationDtos;
            return new JsonResult(reservationToReturn);
        }
        /// <summary>
        /// retrieves a single room by the ID
        /// </summary>
        /// <param name="id">the room ID from the URL</param>
        /// <returns>if successful, returns the room; otherwise, returns an error message</returns>
        /// <response code="200"></response>
        /// <response code="404">no room was found matching that ID</response>
        /// <remarks>
        ///     Example request:
        ///         GET /api/rooms/1
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]


        public JsonResult GetSingleRoom(int id)
        {
            var room = _manager.GetRoom(id);
            if (room == null) return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
            RoomDTO response = room.ConvertToResponseDto(Mapper);
            return new JsonResult(response);
        }/// <summary>
        /// Allows changes to the ConferenceRoom. Name and Location only.
        /// </summary>
        /// <param name="id">the room Id from the Url</param>
        /// <param name="updateRequest">the changes requested to make to the room properties</param>
        /// <returns>if successful, returns the updated room</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

        public IActionResult UpdateRoom(int id, ConferenceRoomUpdateRequest updateRequest)
        {
            var roomToUpdate = _manager.GetRoom(id);
            if (roomToUpdate == null)
            {
                throw new Exception("No Room Found with that Id");
            }
            var returnedRoom = _manager.PatchRoom(roomToUpdate, updateRequest);

            return Ok(returnedRoom);
        }
        /// <summary>
        /// Removes a ConferenceRoom
        /// </summary>
        /// <param name="id">the id of the conferenceRoom to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public void RemoveRoom(int id)
        {
            var roomToDelete = _manager.GetRoom(id);
            if (roomToDelete == null) throw new Exception("Room is either not available or non-existant");
            _manager.RemoveRoom(roomToDelete);
        }
        
    }
   


}
