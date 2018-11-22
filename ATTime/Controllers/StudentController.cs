﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Data.Entity;

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

        // SØGEFUNKTION //

        [HttpPost]
        public ActionResult Index(string firstname)
        {
            ATTime_DBContext db = new ATTime_DBContext();
            var firstnamelist = db.Student.FromSql("select * from student where first_name=@p0", firstname).ToList();

            return View(firstnamelist);
        }
                             
        [HttpPost]
        public ActionResult CreateStudent(Student obj)
        {
            var context = new ATTime_DBContext();
            ATTime_DBContext db = new ATTime_DBContext();

            List<object> lst = new List<object>();
                lst.Add(obj.FirstName);
                lst.Add(obj.LastName);
                lst.Add(obj.Username);
                lst.Add(obj.Psw);
                object[] allstudents = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into student(first_name, last_name, username, psw) values(@p0,@p1,@p2,@p3)", allstudents);
                if(output>0)
                {
                    ViewBag.msg = obj.FirstName + "is added";
                }

                return View(output);
            
            
        } 

    } 
}