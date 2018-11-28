using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Operator
    {
        public Operator()
        {
            CourseOperator = new HashSet<CourseOperator>();
            TeamOperator = new HashSet<TeamOperator>();
        }

        public int OperatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public int? SchoolId { get; set; }

        public Permission Role { get; set; }
        public School School { get; set; }
        public ICollection<CourseOperator> CourseOperator { get; set; }
        public ICollection<TeamOperator> TeamOperator { get; set; }
    }
}
