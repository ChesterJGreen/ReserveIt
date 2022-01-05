using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Request
{
    /// <summary>
    /// this model is used for both registering and updating a user
    /// </summary>
    public class UserAddUpdateRequest
    {
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        [Required]
        [MaxLength(24)]
        [MinLength(8, ErrorMessage ="You need a longer password.")]
        public string Password { get; set; }


    }
}
