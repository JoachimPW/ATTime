using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATTime.Controllers
{
    public class AdminViewController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return View("~/Views/Login/Index");
            }
            else
            {
                if (((string)Session["UserRole"]) == "Student")
                {
                    return View("~/StudentView/Index");
                }
                else if (((string)Session["UserRole"]) == "Teacher")
                {
                    return View("~/TeacherView/Index");
                }
                else if (((string)Session["UserRole"]) == "Admin")
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