using Moq;
using System;
using System.Collections.Generic;
using ReserveIt.Managers;
using AutoMapper;
using ReserveIt.Models;
using ReserveIt.Models.Response;
using Xunit;

namespace ReservationTesting.ControllerTests
{
    public class RoomControllerTests
    {
        private readonly Mock<IConferencesManager> _mockRoomManager;
        private readonly IMapper _mapper;

        private IConferencesManager roomManager => _mockRoomManager.Object;

        private List<ConferenceRoom> conferenceRooms;
        private List<RoomDTO> expectedDTOs;

        private ReserveIt.Config.BaseControllerDependencies dependencies => new ReserveIt.Config.BaseControllerDependencies(_mapper, null);

        public RoomControllerTests()
        {
            var testData = new ReservationTesting.TestData.Rooms();                      
            conferenceRooms = testData.conferenceRooms;
            expectedDTOs = testData.expectedDTOs;

             _mapper = ReserveIt.TestingCommon.AutoMapperTesting.GetMapper();

            _mockRoomManager = new Mock<IConferencesManager>();
            _mockRoomManager.Setup(mgr => mgr.GetRoom(5)).Returns(value: null);
        }
        [Fact]
        public void GetSingleRoom_NotFoundResult()
        
        {
            var controller = new ReserveIt.Controllers.ConferencesController(dependencies, roomManager);

            var result = controller.GetSingleRoom(5);

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<string>(result.Value);
            Assert.False(string.IsNullOrWhiteSpace(result.Value as string));

        }
        [Fact]
        public void GetAllRooms_SuccessResult()
        {
            _mockRoomManager.Setup(mgr => mgr.GetAllRoomsReadOnly()).Returns(conferenceRooms);
            var controller = new ReserveIt.Controllers.ConferencesController(dependencies, roomManager);

            List<Action<RoomDTO>> inspectorsForResult = new List<Action<RoomDTO>>();
            foreach (var expectedDto in expectedDTOs)
            {
                inspectorsForResult.Add(
                    new Action<RoomDTO>(dto => ValidateRoomDTO(expectedDto, dto)));
            }
            var result = controller.GetAllRooms();

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode ?? 200);
            Assert.IsAssignableFrom<IEnumerable<RoomDTO>>(result.Value);

            Assert.Collection<RoomDTO>(result.Value as IEnumerable<RoomDTO>, inspectorsForResult.ToArray());
        }
        private void ValidateRoomDTO(RoomDTO expectedDTO, RoomDTO actualDTO)
        {
            Assert.NotNull(actualDTO);
            Assert.Equal(expectedDTO.Id, actualDTO.Id);
            Assert.Equal(expectedDTO.Name, actualDTO.Name);
            Assert.Equal(expectedDTO.Location, actualDTO.Location);
        }

    }
}
