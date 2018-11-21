using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Attendance
    {
        public Attendance()
        {
            AttendanceCourseStudent = new HashSet<AttendanceCourseStudent>();
        }

        public int AttendanceId { get; set; }
        public string AttendanceName { get; set; }

        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
    }
}
