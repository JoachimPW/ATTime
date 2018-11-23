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

        [Required(ErrorMessage = "Please Enter Your Username")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [Display(Name = "Password")]
        public string Psw { get; set; }

        public School school;
        public string Phone { get; set; }
        public int? RoleId { get; set; }

        public Permission Role { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
    }
}
