using ReserveIt.Models;
using ReserveIt.Models.Response;
using ReserveIt.Utilities;
using Xunit;
using static ReserveIt.Data.Reference;

namespace ReservationTesting
{
    public class ConvertConferenceToRoomDTO
    {
        private readonly ConferenceRoom[] conferenceRooms;
        private readonly RoomDTO[] expectedDTOs;

        public ConvertConferenceToRoomDTO()
        {
            var expectedDTO1 = new RoomDTO(){ Id = 5, Name = "Test Room", Location = "TestingInIdaho"};
            var room1 = new ConferenceRoom()
            {
                Id = expectedDTO1.Id,
                Name = expectedDTO1.Name,
                Location = expectedDTO1.Location,
                ResourceTimeZone = ReserveIt.Enums.ResourceTimeZone.MST,
                BuildingName = "Test Building",
                SeatingProvided = 30,
                ResourceType = ResourceType.ConferenceRoom,
                Reservations = null
                

            };
            var expectedDTO2 = new RoomDTO() { Id = 8, Name = "Test Room 2", Location = "AlsoTestingInIdaho" };
            var room2 = new ConferenceRoom()
            {
                Id = expectedDTO2.Id,
                Name = expectedDTO2.Name,
                Location = expectedDTO2.Location,
                ResourceTimeZone = ReserveIt.Enums.ResourceTimeZone.PST,
                BuildingName = "2nd Test Building",
                SeatingProvided = 40,
                ResourceType = ResourceType.ConferenceRoom,
                Reservations = null
                
            };
            conferenceRooms = new ConferenceRoom[] { room1, room2 };
            expectedDTOs = new RoomDTO[] { expectedDTO1, expectedDTO2 };
        }
        private void ValidateRoomDTO(RoomDTO expectedDTO, RoomDTO actualDTO)
        {
            Assert.NotNull(actualDTO);
            Assert.Equal(expectedDTO.Id, actualDTO.Id);
            Assert.Equal(expectedDTO.Name, actualDTO.Name);
            Assert.Equal(expectedDTO.Location, actualDTO.Location);
        }
        [Fact]
        public void SingleValidData()
        {
            var room = conferenceRooms[0];
            var expectedDTO = expectedDTOs[0];

            var dto = room.ConvertToResponseDto();

            ValidateRoomDTO(expectedDTO, dto);
        }
    }
}
