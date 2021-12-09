using ReserveIt.Models;
using ReserveIt.Models.Response;
using System;
using Xunit;
using Xunit.Abstractions;

namespace ReservationTesting 
{
    public class UnitTest1 : IDisposable
    {
        private readonly RoomDTO _dto;
        private readonly ITestOutputHelper _output;
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
            _dto = new RoomDTO();

           
            _dto.Id = -1;
            _dto.Location = "Nampa, ID";
            _dto.Name = "Nampa Public Library";
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
        
       
        
    }
}
