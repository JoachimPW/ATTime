using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;

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
            using (var context = new ATTime_DBContext())
            {
                var std = new Student()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = psw

                };
                context.Students.Add(std);

                context.SaveChanges();
            }

          return Redirect("Index");            
        }

        public ActionResult Delete(int studentid)
        {
            using (var context = new ATTime_DBContext())
            {
                var students = context.Students.FirstOrDefault(s => s.StudentId == studentid);
                context.Students.Remove(students);
                context.SaveChanges();
            }

            return Redirect("Index");

        }

    } 
}