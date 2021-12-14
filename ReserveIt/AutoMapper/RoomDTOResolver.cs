using AutoMapper;
using ReserveIt.Enums;
using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Mappings
{
    public class RoomDTOResolver : AutoMapper.IValueResolver<ConferenceRoom, RoomDTO, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(ConferenceRoom source, RoomDTO destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            // get devices from source
            var devices = source.AvailableLectureDevices;
            if (source.AvailableLectureDevices.Count() > 1)
                devices = source.AvailableLectureDevices.Where(device => device != AssistedLectureDevices.none);

            // convert each device in the collection into its string name
            IEnumerable<string> names = devices.Select(d => Enum.GetName(typeof(AssistedLectureDevices), d));
            return names;
        }
    }
}
