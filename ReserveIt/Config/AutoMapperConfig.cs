using AutoMapper;
using ReserveIt.Enums;
using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Config
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression configureMe)
        {

            var mapExpRoomToDTO = configureMe.CreateMap<ConferenceRoom, RoomDTO>();
            mapExpRoomToDTO.ForMember(
                dest => dest.AvailableLectureDevices,
                options => options.MapFrom<IEnumerable<string>>(src =>
                    src.AvailableLectureDevices.Where(d => d != AssistedLectureDevices.none)
                    .Select(dev => Enum.GetName(typeof(AssistedLectureDevices), dev))
                ));
            
            configureMe.CreateMap<Reservation, ReservationDTO>();
                     
                
            
        }
    }
}
