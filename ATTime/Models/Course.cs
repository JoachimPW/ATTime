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
            CourseCode = new HashSet<CourseCode>();
            TeamCourseOperator = new HashSet<TeamCourseOperator>();
            TeamCourseStudent = new HashSet<TeamCourseStudent>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseCalender> CourseCalender { get; set; }
        public ICollection<CourseCode> CourseCode { get; set; }
        public ICollection<TeamCourseOperator> TeamCourseOperator { get; set; }
        public ICollection<TeamCourseStudent> TeamCourseStudent { get; set; }
    }
}
