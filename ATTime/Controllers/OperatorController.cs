using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;

namespace ATTime.Controllers
{
    public class OperatorController : Controller
    {
        // GET: Operator
        public ActionResult Index()
        {
            var context = new ATTime_DBContext();

            var admin = context.Operators
                        .FromSql("select * from operator where role_ID=2")
                        .ToList();

            ViewBag.admin = admin;

            var teacher = context.Operators
                        .FromSql("select * from operator where role_ID=1")
                        .ToList();

            ViewBag.teacher = teacher;

            return View();
        }
    }
}