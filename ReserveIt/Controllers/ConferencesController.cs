using Microsoft.AspNetCore.Mvc;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Controllers
{
    [Route("api/conferences")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly ConferencesRepository _repository;
        public ConferencesController(ConferencesRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("list")]
        public  Task<JsonResult> GetAllRooms()
        {
           
               // Data.MockDataLayer.SeedEmptyDatabase();
                return new JsonResult(Data.MockDataLayer.GetConferenceRooms());
           

            
            
        }

        [HttpGet("{id}/availability")]
        public JsonResult GetRoomAvailability(int id)
        {
            var rooms = Data.MockDataLayer.GetConferenceRooms();
            var roomInQuestion = rooms.SingleOrDefault(x => x.Id == id);
            if (roomInQuestion == null)
                return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
            var reservationToReturn = roomInQuestion.Reservations;
            return new JsonResult(reservationToReturn);


        }
        //[HttpPost]
        //public  Task<JsonResult> CreateConferenceRoom([FromBody] ConferenceRoom conferenceRoom)
        //{

        //    try
        //    {
        //        ConferenceRoom toBeCreated = _repository.AddConference(conferenceRoom);
        //        return new JsonResult(toBeCreated);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new JsonResult(BadRequest(ex));
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<JsonResult> EditConferenceRoom(int id, [FromBody] ConferenceRoom venue)
        //{
        //    try
        //    {
        //        var rooms = Data.MockDataLayer.GetConferenceRooms();
        //        var roomInQuestion = rooms.SingleOrDefault(x => x.Id == id);
        //        if (roomInQuestion == null)
        //            return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
        //        ConferenceRoom conferenceRoomToEdit = await Data.MockDataLayer.EditConferenceRoom(id, venue);
        //        return new JsonResult(conferenceRoomToEdit);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new JsonResult(BadRequest(ex));
        //    }
        //}
        //[HttpDelete("{id}")]
        //public async Task<JsonResult> DeleteConferenceRoom(int id)
        //{
        //    try
        //    {
        //        var rooms = Data.MockDataLayer.GetConferenceRooms();
        //        var roomInQuestion = rooms.SingleOrDefault(x => x.Id == id);
        //        if (roomInQuestion == null)
        //            return new JsonResult("Room is either not available or non-existant") { StatusCode = 404 };
        //        ConferenceRoom conferenceRoomToDelete = await Data.MockDataLayer.DeleteConferenceRoom(id);
        //        return new JsonResult("Conference Room has been deleted.");
        //    }
        //    catch (Exception ex)
        //    {

        //        return new JsonResult(BadRequest(ex));
        //    }
        //}
    }

}
