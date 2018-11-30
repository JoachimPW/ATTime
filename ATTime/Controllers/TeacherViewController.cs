using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATTime.Models.LoginViewModels;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;


namespace ATTime.Controllers
{
    public class TeacherViewController : Controller
    {
        public ActionResult Index()
        {
            if (((int)Session["UserId"]) == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
                var context = new ATTime_DBContext();
                var currentid = ((int)Session["UserId"]);
                var currentrole = ((string)Session["UserRole"]);
                var school = ((int)Session["School"]);
                var schoolid = context.Operators.Where(s => s.OperatorId == currentid).Single().SchoolId;
                var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
                var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
                ViewData["id"] = currentid;
                ViewData["Role"] = currentrole;
                ViewData["Schoolname"] = schoolname;
                ViewData["Logo"] = schoollogo;

                //Tilføj koden her:
                var teams_operator = context.TeamOperators
                .Where(s => s.OperatorId == currentid)
                .Include(s => s.Team)
                .ToList();

                ViewBag.TO = teams_operator;


                //Sakffer routen for en bruger
                if (currentrole == "Teacher" && currentid != 0)
                {
                    return View();
                }
                else
                {
                    string url = LoginCheckViewModel.check(currentid, currentrole);
                    return RedirectToAction("Index", url);
                }
            }
        }

        public ActionResult AddCourse(int Courseid, int calendercourseid, int teamid)
        {
            //Informationer
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Operators.Where(s => s.OperatorId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;

            //Koden fra calender
            var start_date = DateTime.Now.ToString("dd-MM-yyyy");
            var startid = context.Calenders.Where(s => s.CalenderName == start_date).Single().CalenderId;
            var teams_calender = context.CourseCalenders
                           .Where(s => s.TeamId == teamid)
                           .Where(s => s.CalenderId >= startid)
                           .Include(s => s.Course)
                           .Include(s => s.Calender)
                            .OrderBy(s => s.CalenderId)
                           .ToList();

            var course_operator = context.CourseOperators
                   .Where(s => s.OperatorId == currentid)
                   .Include(s => s.Course)
                   .ToList();

            ViewBag.TO = teams_calender;
            ViewBag.CO = course_operator;

            //Selven koden til funktionen
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            var today_id = context.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;

            //Skaffer en random kode, som kan bruges til at bekræfte man er der på dagen: 
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 5)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            string authToken = resultToken.ToString();

            using (context)
            {
                var calender = context.CourseCalenders.Where(s => s.CourseCalenderId == calendercourseid).Single().CalenderId;
                var students = context.TeamStudents
                    .Where(s => s.TeamId == teamid)
                    .ToList();
                //opdateret faget i coursecalender for teamet
                var addcourse = context.CourseCalenders
                        .Where(s => s.CourseCalenderId == calendercourseid)
                        .Where(s => s.TeamId == teamid)
                        .FirstOrDefault();
                addcourse.CourseId = Courseid;

                //Opdatere for hver studerende, at de ikke har været der endnu
                foreach (TeamStudent s in students)
                {
                    var check = context.AttendanceCourseStudents.Where(d => d.CalenderId == calender).Where(h => h.StudentId == s.StudentId).Count();
                    if (check > 0)
                    {

                    }
                    else
                    {
                        context.AttendanceCourseStudents.Add(new AttendanceCourseStudent() { AttendanceId = 1, CourseId = Courseid, StudentId = s.StudentId, TeamId = teamid, CalenderId = calender });
                    }
                }


                //tilføjer en kode til dagen, så man kan tjekke ind:
                var code = new CourseCode()
                {
                    Code = authToken,
                    CalenderId = today_id
                };
                context.CourseCodes.Add(code);
                context.SaveChanges();
            }
            //Return
            return View("calender");
        }

        public ActionResult calender(int teamid)
        {
            //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Operators.Where(s => s.OperatorId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;
            ViewData["team"] = teamid;

            //Tilføj koden her:
            var start_date = DateTime.Now.ToString("dd-MM-yyyy");
            var startid = context.Calenders.Where(s => s.CalenderName == start_date).Single().CalenderId;
            var teams_calender = context.CourseCalenders
                           .Where(s => s.TeamId == teamid)
                           .Where(s => s.CalenderId >= startid)
                           .Include(s => s.Course)
                           .Include(s => s.Calender)
                           .OrderBy(s => s.CalenderId)
                           .ToList();

          var course_operator = context.CourseOperators
                 .Where(s => s.OperatorId == currentid)
                 .Include(s => s.Course)
                 .ToList();

          ViewBag.TO = teams_calender;
          ViewBag.CO = course_operator;

            //Sakffer routen for en bruger
            if (currentrole == "Teacher" && currentid != 0)
            {
                return View();
            }
            else
            {
                string url = LoginCheckViewModel.check(currentid, currentrole);
                return RedirectToAction("Index", url);
            }

        }

        public ActionResult between(int teamid, DateTime start, DateTime end)
        {
            //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Operators.Where(s => s.OperatorId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;
            ViewData["team"] = teamid;

            //Tilføj koden her:
            var start_date = start.ToString("dd/MM/yyyy");
            var end_date = end.ToString("dd/MM/yyyy");
            var startid = context.Calenders.Where(s => s.CalenderName == start_date).Single().CalenderId;
            var slutid = context.Calenders.Where(s => s.CalenderName == end_date).Single().CalenderId;
            var teams_calender = context.CourseCalenders
                           .Where(s => s.TeamId == teamid)
                           .Where(s => s.CalenderId > startid)
                           .Where(s => s.CalenderId < slutid)
                           .Include(s => s.Course)
                           .Include(s => s.Calender)
                           .ToList();

            var course_operator = context.CourseOperators
                  .Where(s => s.OperatorId == currentid)
                  .Include(s => s.Course)
                  .ToList();

            ViewBag.TO = teams_calender;
            ViewBag.CO = course_operator;

            //Sakffer routen for en bruger
            if (currentrole == "Teacher" && currentid != 0)
            {
                return View("calender");
            }
            else
            {
                string url = LoginCheckViewModel.check(currentid, currentrole);
                return RedirectToAction("Index", url);
            }

        }


        public ActionResult student(int teamid)
        {
            if (((int)Session["UserId"]) == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
                var context = new ATTime_DBContext();
                var currentid = ((int)Session["UserId"]);
                var currentrole = ((string)Session["UserRole"]);
                var school = ((int)Session["School"]);
                var schoolid = context.Operators.Where(s => s.OperatorId == currentid).Single().SchoolId;
                var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
                var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
                ViewData["id"] = currentid;
                ViewData["Role"] = currentrole;
                ViewData["Schoolname"] = schoolname;
                ViewData["Logo"] = schoollogo;
                ViewData["team"] = teamid;

                //Tilføj koden her:
                var every_student = context.TeamStudents
                    .Where(s => s.TeamId == teamid)
                    .ToList();

                ViewBag.students = every_student;


                //Sakffer routen for en bruger
                if (currentrole == "Teacher" && currentid != 0)
                {
                    return View();
                }
                else
                {
                    string url = LoginCheckViewModel.check(currentid, currentrole);
                    return RedirectToAction("Index", url);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult _Student_attend(int TeamID, int date)
        {
            //Her tjekker vi, som vi har en session med et id i:
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //Kode
                var context = new ATTime_DBContext();
                var course_date = context.Calenders
                    .Where(s => s.CalenderId == date)
                    .Single().CalenderName;
                var Attended = context.AttendanceCourseStudents
                    .Where(s => s.TeamId == TeamID)
                    .Where(s => s.Calender.CalenderName == course_date)
                    .ToList()
                    .OrderBy(s => s.AttendanceId);
                ViewBag.ATT = Attended;
            }
            return PartialView("_Student_attend.cshtml");
        }
    }
}
