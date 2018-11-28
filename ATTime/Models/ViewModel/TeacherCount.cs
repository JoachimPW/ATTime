using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTime.Models.ViewModel
{
    public class TeacherCount
    {
        public static int teacher_count(int? team_id)
        {
            var context = new ATTime_DBContext();

            var team_teacher_count = context.TeamOperators
                        .Where(s => s.TeamId == team_id)
                        .Count();

            return team_teacher_count;
        }
    }
}