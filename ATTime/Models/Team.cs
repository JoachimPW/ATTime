using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Team
    {
        public Team()
        {
            TeamCourseOperator = new HashSet<TeamCourseOperator>();
            TeamCourseStudent = new HashSet<TeamCourseStudent>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
        public ICollection<TeamCourseStudent> TeamCourseStudent { get; set; }
    }
}
