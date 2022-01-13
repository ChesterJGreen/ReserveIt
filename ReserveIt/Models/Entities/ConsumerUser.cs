using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Entities
{
    public class ConsumerUser : IUserInfo
    {
      public int Id { get; set; }

        [NotMapped]
        public string FirstName
        {
            get { return AuthenticatedUser.FirstName; }
            set { AuthenticatedUser.FirstName = value; }
        }
        [NotMapped]
        public string LastName
        {
            get { return AuthenticatedUser.LastName; }
            set { AuthenticatedUser.LastName = value; }
        }
        [NotMapped]
        public string Username
        {
            get { return AuthenticatedUser.Username; }
            set { AuthenticatedUser.Username = value; }
        }
        [MaxLength(50)]
        public string Location { get; set; }
        public bool IsMembershipActive { get; set; } = false;
        public string MembershipLevel { get; set; }
        public int TotalRewardPoints { get; set; } = 0;


        public virtual User AuthenticatedUser { get; private set; }
    }
}
