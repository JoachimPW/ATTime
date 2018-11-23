using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class School
    {
        public School()
        {
            Operator = new HashSet<Operator>();
            Student = new HashSet<Student>();
            Team = new HashSet<Team>();
        }

        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Logo { get; set; }

        public ICollection<Operator> Operator { get; set; }
        public ICollection<Student> Student { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}
