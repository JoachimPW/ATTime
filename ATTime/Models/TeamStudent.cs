using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class TeamStudent
    {
        public int TeamStudentId { get; set; }
        public int? TeamId { get; set; }
        public int? StudentId { get; set; }

        public Student Student { get; set; }
        public Team Team { get; set; }
    }
}
