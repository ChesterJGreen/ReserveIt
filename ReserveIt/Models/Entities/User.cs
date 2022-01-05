using System.ComponentModel.DataAnnotations;

namespace ReserveIt.Models
{
    public class User
    {
        public int Id { get; set; }

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
        [MinLength(8, ErrorMessage = "You need a longer password.")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


    }
}