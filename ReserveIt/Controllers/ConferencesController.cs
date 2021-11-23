using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("list")]
        public JsonResult GetAllRooms()
        {
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
    }

}
