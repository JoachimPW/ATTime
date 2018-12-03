using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class CourseCode
    {
        public int CourseCodeId { get; set; }
        public int? CalenderId { get; set; }
        public string Code { get; set; }
        public int? TeamId { get; set; }

        public Calender Calender { get; set; }
        public Team Team { get; set; }
    }
}
