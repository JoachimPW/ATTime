using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Logo { get; set; }
        public int? TeamId { get; set; }

        public Team Team { get; set; }
    }
}
