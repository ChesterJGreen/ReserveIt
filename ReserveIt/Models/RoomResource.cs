using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public class RoomResource : BaseResource
    {
        public RoomResource()
        {
            base.ResourceType = Data.Reference.ResourceType.ConferenceRoom;
        }
        [MaxLength(20)]
        public string Location { get; set; }
    }
}
