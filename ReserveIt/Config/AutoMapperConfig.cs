using AutoMapper;
using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression configureMe)
        {

            configureMe.CreateMap<ConferenceRoom, RoomDTO>();
            configureMe.CreateMap<Reservation, ReservationResponse>();
                     
                
            
        }
    }
}
