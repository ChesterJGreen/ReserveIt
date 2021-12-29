using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationTesting.TestData
{
    public class Rooms
    {
        public readonly List<ConferenceRoom> conferenceRooms = new List<ConferenceRoom>();
        public readonly List<RoomDTO> expectedDTOs = new List<RoomDTO>();

        public Rooms()
        {
            var expectedDTO1 = new RoomDTO() { Id = 5, Name = "Test Room", Location = "InsideTest" };
            var room1 = new ConferenceRoom()
            {
                Id = expectedDTO1.Id,
                Name = expectedDTO1.Name,
                Location = expectedDTO1.Location,
                ResourceType = ReserveIt.Data.Reference.ResourceType.ConferenceRoom,
                SeatingProvided = 30,
                AvailableLectureDevices = new ReserveIt.Enums.AssistedLectureDevices[] { ReserveIt.Enums.AssistedLectureDevices.microphone, ReserveIt.Enums.AssistedLectureDevices.whiteboard },
                ResourceTimeZone = ReserveIt.Enums.ResourceTimeZone.MST



            };
            var expectedDTO2 = new RoomDTO() { Id = 2, Name = "Test Blue Room", Location = "OutsideTest" };
            var room2 = new ConferenceRoom()
            {
                Id = expectedDTO2.Id,
                Name = expectedDTO2.Name,
                Location = expectedDTO2.Location,
                ResourceTimeZone = ReserveIt.Enums.ResourceTimeZone.MST,
                ResourceType = ReserveIt.Data.Reference.ResourceType.ConferenceRoom,
                SeatingProvided = 20,
                BuildingName = "TestBuilding2"

            };

            conferenceRooms.Add(room1);
            conferenceRooms.Add(room2);
            expectedDTOs.Add(expectedDTO1);
            expectedDTOs.Add(expectedDTO2);
        }
    }
}
