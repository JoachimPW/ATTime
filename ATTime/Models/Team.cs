using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Team
    {
        public Team()
        {
            AttendanceCourseStudent = new HashSet<AttendanceCourseStudent>();
            CourseCalender = new HashSet<CourseCalender>();
            CourseCode = new HashSet<CourseCode>();
            TeamOperator = new HashSet<TeamOperator>();
            TeamStudent = new HashSet<TeamStudent>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseCalender> CourseCalender { get; set; }
        public ICollection<CourseCode> CourseCode { get; set; }
        public ICollection<TeamOperator> TeamOperator { get; set; }
        public ICollection<TeamStudent> TeamStudent { get; set; }
    }
}
