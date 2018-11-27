﻿using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Calender
    {
        public Calender()
        {
            AttendanceCourseStudent = new HashSet<AttendanceCourseStudent>();
            CourseCalender = new HashSet<CourseCalender>();
        }

        public int CalenderId { get; set; }
        public string CalenderName { get; set; }

        public ICollection<AttendanceCourseStudent> AttendanceCourseStudent { get; set; }
        public ICollection<CourseCalender> CourseCalender { get; set; }
    }
}
