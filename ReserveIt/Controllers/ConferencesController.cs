﻿using Microsoft.AspNetCore.Mvc;
using ReserveIt.Data;
using ReserveIt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserveIt.Managers;
using ReserveIt.Models.Response;
using ReserveIt.Utilities;
using AutoMapper;

namespace ReserveIt.Controllers
{
    [Route("api/conferences")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferencesManager _manager;
        private readonly IMapper _mapper;
        public ConferencesController(IConferencesManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        [HttpGet("list")]
        public JsonResult GetAllRooms()
        {
            var conferenceRooms = _mapper.Map<IEnumerable<RoomDTO>>(_manager.GetAllRoomsReadOnly());
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
            RoomDTO response = room.ConvertToResponseDto();
            return new JsonResult(response);
        }
        
    }
   


}
