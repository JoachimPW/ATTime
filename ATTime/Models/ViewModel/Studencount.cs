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

        public static int total_absent(int studentid, int teamid)
        {
            var context = new ATTime_DBContext();
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            var today_id = context.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;
            var all_courses_adsent = context.AttendanceCourseStudents
                        .Where(s => s.StudentId == studentid)
                        .Where(s => s.TeamId == teamid)
                        .Where(s => s.AttendanceId == 1)
                        .Where(s => s.CalenderId < today_id)
                        .Count();
            var all_courses = context.AttendanceCourseStudents
                        .Where(s => s.StudentId == studentid)
                        .Where(s => s.TeamId == teamid)
                        .Where(s => s.CalenderId < today_id)
                        .Count();
            int absense = (all_courses_adsent / all_courses) * 100;
            return absense;
        }

        public static int total_absent_course(int? studentid, int? courseid)
        {
            var context = new ATTime_DBContext();
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            var today_id = context.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;
            var all_courses_adsent = context.AttendanceCourseStudents
                        .Where(s => s.StudentId == studentid)
                        .Where(s => s.CourseId == courseid)
                        .Where(s => s.AttendanceId == 1)
                        .Where(s => s.CalenderId < today_id)
                        .Count();
            var all_courses = context.AttendanceCourseStudents
                        .Where(s => s.StudentId == studentid)
                        .Where(s => s.CourseId == courseid)
                        .Where(s => s.CalenderId < today_id)
                        .Count();
            int absense = (all_courses_adsent / all_courses) * 100;
            return absense;
        }
    }
}