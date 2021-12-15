using ReserveIt.Models;
using ReserveIt.Models.Response;
using ReserveIt.Utilities;
using System;
using System.Collections.Generic;
using Xunit;
using static ReserveIt.Data.Reference;

namespace ReservationTesting
{
    public class ConvertConferenceToRoomDTOAutoMapperTests
    {
        private ConferenceRoom[] conferenceRooms;
        private RoomDTO[] expectedDTOs;
        private AutoMapper.IMapper mapper;

        public ConvertConferenceToRoomDTOAutoMapperTests()
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

            var configuration = new AutoMapper.MapperConfiguration(
                config => ReserveIt.Config.AutoMapperConfig.ConfigureAutoMapper(config)
                );
            mapper = configuration.CreateMapper();
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

            var dto = room.ConvertToResponseDto(mapper);

            ValidateRoomDTO(expectedDTO, dto);
        }
        [Fact]
        public void SingleEmptyRoomObject()
        {
            var emptyRoom = new ConferenceRoom();

            Assert.Throws<ArgumentNullException>(() =>
            {
                emptyRoom.ConvertToResponseDto(mapper);
            });
        }
        [Fact]
        public void SingleNullRoomObject()
        {
            ConferenceRoom nullRoom = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
               nullRoom.ConvertToResponseDto(mapper);
            });
        }
        [Fact]
        public void CollectionNull()
        {
            IEnumerable<ConferenceRoom> nullCollection = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                nullCollection.ConvertToResponseDto(mapper);
            });
        }
        [Fact]
        public void CollectionEmpty()
        {
            IEnumerable<ConferenceRoom> emptyCollection = new ConferenceRoom[0];
            var result = emptyCollection.ConvertToResponseDto(mapper);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public void CollectionOneRoom()
        {
            var oneRoom = new ConferenceRoom[] { conferenceRooms[0] };
            var expectedDto = expectedDTOs[0];

            var actualDto = oneRoom.ConvertToResponseDto(mapper);
            var inspectorsForResult = new Action<RoomDTO>[]
            {
                new Action<RoomDTO>(dto => ValidateRoomDTO(expectedDto, dto))
            };
            Assert.NotNull(actualDto);
            Assert.Collection<RoomDTO>(actualDto, inspectorsForResult);

        }
        [Fact]
        public void CollecitonMultipleRooms()
        {
            var dto = conferenceRooms.ConvertToResponseDto(mapper);
            List<Action<RoomDTO>> inspectorsForResult = new List<Action<RoomDTO>>();
            foreach (var expectedDto in expectedDTOs)
            {
                inspectorsForResult.Add(
                    new Action<RoomDTO>(dto => ValidateRoomDTO(expectedDto, dto)));

            }

            Assert.NotNull(dto);
            Assert.Collection<RoomDTO>(dto, inspectorsForResult.ToArray());

        }

        [Fact]
        public void BadMapShouldFail()
        {
            var room = conferenceRooms[0];
            Assert.Throws<AutoMapper.AutoMapperMappingException>(() => mapper.Map<ReservationDTO>(room));
                }
    }
}
