using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Operator
    {
        public Operator()
        {
            TeamCourseOperator = new HashSet<TeamCourseOperator>();
        }

        public int OperatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }

        public Permission Role { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
    }
}
