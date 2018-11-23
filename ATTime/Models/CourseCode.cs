using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class CourseCode
    {
        public int CourseCodeId { get; set; }
        public int? CourseId { get; set; }
        public string Code { get; set; }

        public Course Course { get; set; }
    }
}
