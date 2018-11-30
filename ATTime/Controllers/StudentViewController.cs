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
    public class StudentViewController : Controller
    {
        public ActionResult Index()
        {
            //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Students.Where(s => s.StudentId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            int? team = context.TeamStudents.Where(s => s.StudentId == currentid).Single().TeamId;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;
            ViewData["team"] = team;

            //Tilføj alt koden her
           var today = DateTime.Now.ToString("dd-MM-yyyy");
            var today_id = context.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;
            var today_course = context.CourseCalenders
                 .Where(s => s.CalenderId == today_id)
                 .Where(s => s.TeamId == team)
                 .Single().CourseId;
             var today_course_id = context.CourseCalenders
               .Where(s => s.CalenderId == today_id)
               .Where(s => s.TeamId == team)
               .Single().CourseId;
             ViewData["TC"] = today_course;
             ViewData["CID"] = today_course_id;

             var student_courses = context.CourseStudents
                 .Where(s => s.StudentId == currentid)
                 .ToList();
             ViewBag.Student_courses = student_courses;

             var calender = context.CourseCalenders
                 .Where(s => s.TeamId == team)
                 .ToList();
            ViewBag.calender = calender; 
       
            //Sakffer routen for en bruger
            if (currentrole == "Student" && currentid != 0)
            {
                return View();
            }
            else
            {
                string url = LoginCheckViewModel.check(currentid, currentrole);
                return RedirectToAction("Index", url);
            }
        }

        public ActionResult attend(string code_for_torday)
        {
            //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Students.Where(s => s.StudentId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            int? team = context.TeamStudents.Where(s => s.StudentId == currentid).Single().TeamId;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;
            ViewData["team"] = team;

            //Koden er gengivet fra index af, så alt det samme information kommer med igen, når man trykker tilmeld.
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            var today_id = context.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;
            string today_course = context.CourseCalenders
                .Where(s => s.CalenderId == today_id)
                .Where(s => s.TeamId == team)
                .Single().Course.CourseName;
            var today_course_id = context.CourseCalenders
              .Where(s => s.CalenderId == today_id)
              .Where(s => s.TeamId == team)
              .Single().Course.CourseId;
            ViewData["TC"] = today_course;
            ViewData["CID"] = today_course_id;

            var student_courses = context.CourseStudents
                .Where(s => s.StudentId == currentid)
                .ToList();
            ViewBag.Student_courses = student_courses;

            var calender = context.CourseCalenders
                .Where(s => s.TeamId == team)
                .ToList();
            ViewBag.calender = calender;

            //Koden som denne action skal bruge
            var acsID = context.AttendanceCourseStudents
                .Where(s => s.CalenderId == today_id)
                .Where(d => d.StudentId == currentid)
                .FirstOrDefault().AttendanceCourseStudentId;
            var check_code = context.CourseCodes
              .Where(s => s.CalenderId == today_id)
              .Single().Code;
            if (code_for_torday == check_code)
            {
                using (context)
                {
                    var std = context.AttendanceCourseStudents
                        .Where(s => s.AttendanceCourseStudentId == acsID)
                        .FirstOrDefault();
                    std.AttendanceId = 2;
                    context.SaveChanges();
                }
                ViewBag.msg = "You have attended the course.";
            }
            else
            {
                ViewBag.msg = "You didn't use the right code for todays course.";
            }

            //Sakffer routen for en bruger
            if (currentrole == "Student" && currentid != 0)
            {
                return View("index");
            }
            else
            {
                string url = LoginCheckViewModel.check(currentid, currentrole);
                return RedirectToAction("Index", url);
            }
        }

        public ActionResult procentage()
        {
            //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolid = context.Students.Where(s => s.StudentId == currentid).Single().SchoolId;
            var schoollogo = context.Schools.Where(s => s.SchoolId == schoolid).Single().Logo;
            var schoolname = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            int? team = context.TeamStudents.Where(s => s.StudentId == currentid).Single().TeamId;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;
            ViewData["team"] = team;

            //Tilføj alt koden her
            var courses = context.CourseStudents
                .Where(s => s.StudentId == currentid)
                .Include(s => s.Course)
                .ToList();

            ViewBag.course = courses;

            //Sakffer routen for en bruger
            if (currentrole == "Student" && currentid != 0)
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
}