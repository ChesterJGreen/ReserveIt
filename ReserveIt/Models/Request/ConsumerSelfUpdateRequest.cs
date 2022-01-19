using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Request
{
    public class ConsumerSelfUpdateRequest
    {
        [MaxLength(60)]
        public string? Username { get; set; }

        [MaxLength(60)]
        public string? FirstName { get; set; }

        [MaxLength(60)]
        public string? LastName { get; set; }
        
        [MaxLength(50)]
        public string? Location { get; set; }
    }
}
