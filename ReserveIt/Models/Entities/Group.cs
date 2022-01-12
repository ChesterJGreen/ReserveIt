using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Models.Entities
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        public virtual HashSet<GroupRoleMapping> GroupRoleMappings { get; set; } = new HashSet<GroupRoleMapping>();
    }
}
