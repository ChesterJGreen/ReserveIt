using ReserveIt.Models.Response;
using System;
using Xunit;

namespace ReservationTesting
{
    public class UnitTest1
    {
        [Fact]
        public void GetRoomIsNull()
        {
            RoomDTO testRoom = new RoomDTO();
            testRoom.Id = 1;
            testRoom.Location = "Nampa, ID";
            testRoom.Name = "Nampa Public Library";
            Assert.Null(testRoom);
        }
    }
}
