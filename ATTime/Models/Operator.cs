using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATTime.Models
{
    public partial class Operator
    {
        public Operator()
        {
            TeamCourseOperator = new HashSet<TeamCourseOperator>();
        }

        public int OperatorId { get; set; }
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public int? SchoolId { get; set; }

        public Permission Role { get; set; }
        public School School { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
    }
}
