﻿using System;
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
        public ViewResult Index()
        {
            if (Session["UserId"] == null)
            {
                return View("~/Views/Login/Index");
            }
            else
            {
                if (((string)Session["UserRole"]) == "Teacher")
                {
                    return View("~/TeacherView/Index");
                }
                else if (((string)Session["UserRole"]) == "Admin")
                {
                    return View("~/AdminView/Index");
                }
                else if (((string)Session["UserRole"]) == "Student")
                {
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    return View();
                }
                else
                {
                    return View("~/Views/Login/Index");
                }
            }
        }
    }
}