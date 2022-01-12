using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Entities
{
    public class GroupRoleMapping
    {
        public int Id { get; set; }

        public virtual Group Group { get; set; }
        public virtual Role Role { get; set; }
    }
}
