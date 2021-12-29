using ReserveIt.Models;
using ReserveIt.Models.Response;
using ReserveIt.Managers;
using System;
using Xunit;
using Xunit.Abstractions;
using Moq;
using ReserveIt.Controllers;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace ReservationTesting 
{
    public class UnitTest1 : IDisposable
    {
        private readonly RoomDTO _dto;
        private readonly ITestOutputHelper _output;
        private readonly Mock<IConferencesManager> _mockRoomManager;
        private readonly IMapper _mapper;
        private IConferencesManager roomManager => _mockRoomManager.Object;
        private List<ConferenceRoom> roomResources;
        private List<RoomDTO> expectedDTOs;

        private IConferencesManager conferencesManager => _mockRoomManager.Object;
        public UnitTest1(ITestOutputHelper output)
        {
            var testData = new ReservationTesting.TestData.Rooms();
            var roomResources = testData.conferenceRooms;
            var expectedDTOs = testData.expectedDTOs;

            _mapper = ReserveIt.TestingCommon.AutoMapperTesting.GetMapper();

            _output = output;
            _dto = new RoomDTO();

           

            _mockRoomManager = new Mock<IConferencesManager>();
            _mockRoomManager.Setup(mgr => mgr.GetRoom(5)).Returns(value: null);
        }
        public void Dispose()
        {
            _output.WriteLine($"Disposing RoomDTO {_dto.Name}");
        }
        

        [Fact]
        public void GetRoom_NotNull()
        {
            
            Assert.NotNull(_dto);
        }
        [Fact]
        public void GetRoom_IdExists()
        {
            _output.WriteLine($"Here is the id {_dto.Id}");
            Assert.IsType<int>(_dto.Id);
        }
        [Fact]
        public void GetRoom_LocationExists()
        {
            _output.WriteLine($"Here is the Location {_dto.Location}");
            Assert.NotNull(_dto.Location);
        }
        [Fact]
        public void GetSingleRoomNotFoundResult()
        {
           
            var controller = new ConferencesController(_mockRoomManager.Object, null);
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
            _mockRoomManager.Setup<List<ConferenceRoom>>(mgr => mgr.GetAllRoomsReadOnly()).Returns(roomResources);
            var controller = new ConferencesController(roomManager, _mapper);

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

            /*
            // this is manually doing the collection test above
            //1 - the collection as a whole
            Assert.NotNull(result.Value);
            IEnumerable<RoomDTO> roomDTOs = result.Value as IEnumerable<RoomDTO>;
            Assert.Equal(expectedDTOs.Count, roomDTOs.Count());
            //2 - each element in the collection
            for (int i = 0; i < expectedDTOs.Count; i++)
            {
                ValidateRoomDTO(expectedDTOs[i], roomDTOs.ElementAt(i));
            }
            */
            
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
