using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Text;

namespace ATTime.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            var context = new ATTime_DBContext();
           
            var student = context.Students
                        .FromSql("select * from student")
                        .ToList();

            ViewBag.student = student;

            return View();
        }

        //Opret af bruger, her bruger vi using, til at definere database conn.                 
        [HttpPost]
        public ActionResult CreateStudent(string firstname, string lastname, string username, string psw)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var std = new Student()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw

                };
                context.Students.Add(std);

                context.SaveChanges();
            }

          return Redirect("Index");            
        }

        [HttpPost]
        public ActionResult Delete(int studentid)
        {
            
            if (studentid == 0)
            {
                ViewData["msg"] = "Id not found";
            }

            using (var context = new ATTime_DBContext())
            {
                var students = context.Students.FirstOrDefault(s => s.StudentId == studentid);
                var teamStudents = context.TeamStudents.Single(s => s.StudentId == studentid);
                var courseStudents = context.CourseStudents.Single(s => s.StudentId == studentid);

                if (students == null)
                {
                    ViewData["msg"] = "Student not found";
                }

                context.CourseStudents.Remove(courseStudents);
                context.TeamStudents.Remove(teamStudents);
                context.Students.Remove(students);
                
                context.SaveChanges();
            }
            return RedirectToAction("Student", "Register");
        }

    } 
}