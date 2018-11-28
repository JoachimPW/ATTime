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
            CourseOperator = new HashSet<CourseOperator>();
            CourseStudent = new HashSet<CourseStudent>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseCalender> CourseCalender { get; set; }
        public ICollection<CourseOperator> CourseOperator { get; set; }
        public ICollection<CourseStudent> CourseStudent { get; set; }
    }
}
