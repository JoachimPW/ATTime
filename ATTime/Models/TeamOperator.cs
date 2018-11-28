using System;
using System.Collections.Generic;

namespace ATTime.Models
{
    public partial class TeamOperator
    {
        public int TeamOperatorId { get; set; }
        public int? TeamId { get; set; }
        public int? OperatorId { get; set; }

        public Operator Operator { get; set; }
        public Team Team { get; set; }
    }
}
