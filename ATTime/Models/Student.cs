using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Student
    {
        public Student()
        {
            AttendanceCourseStudent = new HashSet<AttendanceCourseStudent>();
            CourseStudent = new HashSet<CourseStudent>();
            TeamStudent = new HashSet<TeamStudent>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public int? SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseStudent> CourseStudent { get; set; }
        public ICollection<TeamStudent> TeamStudent { get; set; }
    }
}
