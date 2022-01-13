using ReserveIt.Models.Entities;
using System.Collections.Generic;
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

        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public virtual HashSet<UserRoleMapping> UserRoles { get; set; } = new HashSet<UserRoleMapping>();
        public virtual HashSet<UserGroupMapping> UserGroups { get; set; } = new HashSet<UserGroupMapping>();
    }
}