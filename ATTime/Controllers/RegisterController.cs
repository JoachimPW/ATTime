﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ATTime.Controllers
{
    public class RegisterController : Controller
    {
        ATTime_DBContext db = new ATTime_DBContext();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Team()
        {
            var schoolid = ((int)Session["School"]);
            var team = db.Teams.Where(s => s.SchoolId == schoolid);
            ViewBag.team = team;
            return View();
        }
        [HttpPost]
        public ActionResult SelectTeam(int teamid)
        {
            ViewBag.testTeamId = teamid;
            Session["TeamId"] = teamid;
            return RedirectToAction("Student");
        }

        public ActionResult Student()
        {
            var teamId = ((int)Session["TeamId"]);
            ViewBag.teamid = teamId;
            ViewData["TEAMID"] = teamId;

            var schoolid = ((int)Session["School"]);

            var test = db.TeamStudents.Where(t => t.TeamId == teamId).Include(t => t.Student);

            var studentliste = db.Students.Where(s => s.SchoolId == schoolid);

            ViewBag.studentListe = studentliste;

            ViewBag.tester = test;




            /* if(tcs > 1)
             { 
             var student = db.Students.Where(s => s.StudentId == tcs);
             ViewBag.studentListe = student;
             } else
             {
            var student = db.Students.Where(s => s.SchoolId == schoolid);
                ViewBag.studentListe = student;
                foreach (TeamCourseStudent s in students)
            {
                var student = db.Students.Where(a => a.SchoolId == schoolid);
            }
                */

            //var studentsInTable = db.Teams.Where(c => c.TeamId == teamId).SelectMany(c => c.TeamCourseStudent);


            //var zzzz = db.TeamCourseStudents.Where(c => c.TeamId == teamId).SelectMany(c => c.Student);




            //  /db.TeamCourseStudents.Where(s => s.TeamId == teamId).Where(db.Students.Where());


            // ViewBag.student = studenterliste;

            var schoolName = db.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["schoolname"] = schoolName;
            var adminUsername = ((string)Session["adminName"]);
            var adminFirstname = db.Operators.Where(s => s.Username == adminUsername).Single().FirstName;
            var adminLastname = db.Operators.Where(s => s.Username == adminUsername).Single().LastName;
            ViewData["adminFirstname"] = adminFirstname;
            ViewData["adminLastname"] = adminLastname;

            var studentList = db.Students.ToList();
            ViewBag.STUDENT = studentList;

            return View();
        }

        public ActionResult Teacher()
        {
            var schoolid = ((int)Session["School"]);
            var oprtr = db.Operators.Where(s => s.SchoolId == schoolid);
            ViewBag.oprtr = oprtr;

            return View();
        }
        /*[HttpPost]
        public ActionResult _SearchStudent()
        {
            return View(db.Students.ToList());
        } */

        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.Operators.Where(x => x.Username == userdata).SingleOrDefault();
            if(SearchData!=null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        [HttpPost]
        public ActionResult CreateTeacher(string firstname, string lastname, string username, string psw, string phone)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);
            var schoolid = ((int)Session["School"]);


            using (var context = new ATTime_DBContext())
            {               
                var teacher = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,
                    Phone = phone,
                    RoleId = 2,
                    SchoolId = schoolid
                };

                if (ModelState.IsValid)
                {
                    context.Operators.Add(teacher);
                    ViewBag.SuccessMessage = "The teacher: " + "<" + username + ">" + " has been created";
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Teacher");
        }

        [HttpPost]        
        public ActionResult CreateOperator(string schoolname, string logo, string firstname, string lastname, string username, string psw, string phone)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var school = new School()
                {
                    SchoolName = schoolname,
                    Logo = logo
                };
                context.Schools.Add(school);
                int latestId = school.SchoolId;

                var oprtr = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,
                    Phone = phone,
                    RoleId = 1,
            };

                if (ModelState.IsValid)
                {
                    context.Operators.Add(oprtr);
                    ViewBag.SuccessMessage = "The admin: " + "<" + username + ">" + " and the school: " + "<" + schoolname + ">" + " have been created";                               
                    context.SaveChanges();
                }           
                
            }
            return View("Admin");
        }       

        [HttpPost]
        public ActionResult CreateStudent(string firstname, string lastname, string username, string psw)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var schoolid = ((int)Session["School"]);
                var teamId = ((int)Session["TeamId"]);

                var student = new Student()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,
                    SchoolId = schoolid,
                };

                context.Students.Add(student);
                int latestStudentId = student.StudentId;
                var teamStudent = new TeamStudent()
                {
                    TeamId = teamId,
                    StudentId = latestStudentId
                };

                var courseStudent = new CourseStudent()
                {
                    CourseId = 1,
                    StudentId = latestStudentId
                };

                if (ModelState.IsValid)
                {
                    context.TeamStudents.Add(teamStudent);
                    context.CourseStudents.Add(courseStudent);
                    ViewBag.SuccessMessage2 = "The student: " + "<" + username + ">" + " has been created";
                    context.SaveChanges();
                }
                var schoolName = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
                ViewData["schoolname"] = schoolName;
            }
            return RedirectToAction("Student");
        }
        
        public ActionResult CreateTeam(string teamname)

        {
            using (var context = new ATTime_DBContext())
            {
                var schoolid = ((int)Session["School"]);

                var team = new Team()
                {
                    TeamName = teamname,
                    SchoolId = schoolid
                };

                if(ModelState.IsValid)
                {
                    context.Teams.Add(team);
                    context.SaveChanges();
                }
            }    
           
        return RedirectToAction("Team");
        }
        /* SØGEFUNKTION  

        [HttpPost]
        public ActionResult Index(string firstname)
        {
            ATTime_DBContext db = new ATTime_DBContext();
            var firstnamelist = db.Students.FromSql("select * from student where first_name=@p0", firstname).ToList();
            return View(firstnamelist);
        }*/


    }
   
}