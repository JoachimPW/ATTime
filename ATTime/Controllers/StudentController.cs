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

    }
}