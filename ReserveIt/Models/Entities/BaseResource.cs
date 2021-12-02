using ReserveIt.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models
{
    public abstract class BaseResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Data.Reference.ResourceType ResourceType { get; set; }
        public ResourceTimeZone ResourceTimeZone { get; set; }
        [ForeignKey("ResourceId")]
        public virtual IEnumerable<ReservationDto> ReservationDtos { get; set; }
    }
}
