using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class Calender
    {
        public Calender()
        {
            CourseCalender = new HashSet<CourseCalender>();
        }

        public int CalenderId { get; set; }
        public string CalenderName { get; set; }

        public ICollection<CourseCalender> CourseCalender { get; set; }
    }
}
