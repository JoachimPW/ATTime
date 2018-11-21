using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Operator = new HashSet<Operator>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Operator> Operator { get; set; }
    }
}
