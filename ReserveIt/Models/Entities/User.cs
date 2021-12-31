using System.ComponentModel.DataAnnotations;

namespace ReserveIt.Managers
{
    public class User
    {

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        [Required]
        [MaxLength(24)]
        [MinLength(8, ErrorMessage = "You need a longer password.")]]
        public string Password { get;set }


    }
}