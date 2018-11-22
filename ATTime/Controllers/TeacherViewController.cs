using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATTime.Controllers
{
    public class TeacherViewController : Controller
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
                else if (((string)Session["UserRole"]) == "Admin")
                {
                    return View("~/AdminView/Index");
                }
                else if (((string)Session["UserRole"]) == "Teacher")
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