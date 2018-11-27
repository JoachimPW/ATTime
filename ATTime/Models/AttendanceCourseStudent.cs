using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class AttendanceCourseStudent
    {
        public int AttendanceCourseStudentId { get; set; }
        public int? AttendanceId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }
        public int? TeamId { get; set; }

        public Attendance Attendance { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public Team Team { get; set; }
    }
}
