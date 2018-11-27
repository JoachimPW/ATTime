using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class CourseCalender
    {
        public int CourseCalenderId { get; set; }
        public int? CourseId { get; set; }
        public int? CalenderId { get; set; }
        public int? SchoolId { get; set; }
        public int? TeamId { get; set; }

        public Calender Calender { get; set; }
        public Course Course { get; set; }
        public School School { get; set; }
        public Team Team { get; set; }
    }
}
