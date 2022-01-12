using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Entities
{
    public class UserGroupMapping
    {
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
