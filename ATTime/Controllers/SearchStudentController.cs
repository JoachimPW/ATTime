using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Data.Entity;

namespace ATTime.Controllers
{
    public class SearchStudentController : Controller
    {
        // GET: SearchStudent
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
        public ActionResult Index(string firstname)
        {
            ATTime_DBContext db = new ATTime_DBContext();
            var firstnamelist = db.Student.FromSql("select * from student where first_name=@p0", firstname).ToList();

            return View(firstnamelist);
        }
    }
}