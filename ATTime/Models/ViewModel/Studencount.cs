using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTime.Models.ViewModel
{
    public class Studencount
    {
        public static int student_count(int? team_id)
        {
            var context = new ATTime_DBContext();

            var team_student_count = context.TeamStudents
                        .Where(s => s.TeamId == team_id)
                        .Count();

            return team_student_count;
        }
    }
}