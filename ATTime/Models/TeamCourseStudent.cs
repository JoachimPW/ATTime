using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class TeamCourseStudent
    {
        public int TeamCourseStudentId { get; set; }
        public int? TeamId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
        public Team Team { get; set; }
    }
}
