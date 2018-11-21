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
           
            var student = context.Student
                        .FromSql("select * from student")
                        .ToList();

            ViewBag.student = student;
           
            return View();
        }
        [HttpPost]
        public ActionResult CreateStudent(Student collection)
        {
            var context = new ATTime_DBContext();
            try
            {
                List<object> lst = new List<object>();
                lst.Add(collection.FirstName);
                lst.Add(collection.LastName);
                lst.Add(collection.Username);
                lst.Add(collection.Psw);
                object[] allstudents = lst.ToArray();
                int output = context.Database.ExecuteSqlCommand("insert into students(first_name, last_name, username, psw) values(@p0,@p1,@p2,@p3)", allstudents);
                if(output>0)
                {
                    ViewBag.msg = collection.FirstName + "is added";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}