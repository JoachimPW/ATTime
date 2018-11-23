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
            //Her tjekker vi, som vi har en session med et id i:
            if (Session["UserId"] == null)
            {
                return View("~/Views/Login/Index");
            }
            else
            {
                //Her tjekker vi vores role, og sender en person til et andet vi, hvis de ikke har den rigtige role:
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
                    //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    //Tilføj koden her: 
                    ViewBag.start = DateTime.Now.ToString("dd/MM/yyyy");
                    ViewBag.end = DateTime.Now.AddDays(182).ToString("dd/MM/yyyy");


                    //Koden skal slutte her
                    return View();
                }
                else
                {
                    return View("~/Views/Login/Index");
                }
            }
        }

        public ActionResult AddCalender(DateTime start_date, DateTime end_date)
        {
            var today = DateTime.Now;

            int days_between_start = (start_date - today).Days;
            int days_between_end = (end_date - today).Days;


            for (int i = days_between_start; i < days_between_end; i++)
            {
                DateTime.Now.AddDays(i).ToString("dd/MM/yyyy");
            }

            return View("Calender");
        }

        public ActionResult Calender()
        {
            return View();
        }

    }
}