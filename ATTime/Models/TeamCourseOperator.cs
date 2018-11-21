using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class TeamCourseOperator
    {
        public int TeamCourseOperatorId { get; set; }
        public int? TeamId { get; set; }
        public int? CourseId { get; set; }
        public int? OperatorId { get; set; }

        public Course Course { get; set; }
        public Operator Operator { get; set; }
        public Team Team { get; set; }
    }
}
