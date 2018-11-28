using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class CourseOperator
    {
        public int CourseOperatorId { get; set; }
        public int? CourseId { get; set; }
        public int? OperatorId { get; set; }

        public Course Course { get; set; }
        public Operator Operator { get; set; }
    }
}
