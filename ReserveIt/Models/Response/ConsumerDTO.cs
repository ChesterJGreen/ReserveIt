using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Response
{
    public class ConsumerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public bool IsMembershipActive { get; set; }
        public string MembershipLevel { get; set; }
        public int TotalRewardPoints { get; set; }
    }
}
