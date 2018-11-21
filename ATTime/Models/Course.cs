using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Course
    {
        public Course()
        {
            AttendanceCourseStudent = new HashSet<AttendanceCourseStudent>();
            CourseCalender = new HashSet<CourseCalender>();
            TeamCourseOperator = new HashSet<TeamCourseOperator>();
            TeamCourseStudent = new HashSet<TeamCourseStudent>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseCalender> CourseCalender { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
        public ICollection<TeamCourseStudent> TeamCourseStudent { get; set; }
    }
}
