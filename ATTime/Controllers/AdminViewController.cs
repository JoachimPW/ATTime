using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATTime.Models.LoginViewModels;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Data.SqlClient;

namespace ATTime.Controllers
{
    public class AdminViewController : Controller
    {
        public ActionResult Index()
        {

            //Her tjekker vi, som vi har en session med et id i:
            if (Session["UserId"] == null)
            {
                //får default route
                string routeName = ControllerContext.RouteData.Values["Default"].ToString();
                return View(routeName);
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
                    //får default route
                    string routeName = ControllerContext.RouteData.Values["Default"].ToString();
                    return View(routeName);
                }
            }
        }

        public ActionResult AddCalender(DateTime start_date, DateTime end_date)
        {
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            var today = DateTime.Now;
            var datestring = start_date.ToString("dd/MM/yyyy");

            ViewBag.msg = "Add days to your calender";

            if (start_date == null || end_date == null)
            {
                ViewBag.msg = "Add start and end date";
            }
            else
            { 
                int days_between_start = (start_date - today).Days;
                int days_between_end = (end_date - today).Days;

                if(days_between_end < days_between_start)
                {
                    ViewBag.msg = "End date has to be later than Start date";
                }
                else
                {
                    var context = new ATTime_DBContext();
                    var datecount = context.Calenders
                           .Where(s => s.CalenderName == datestring)
                           .Count();

                    if (datecount > 0)
                    {
                        ViewBag.msg = "Date already in calender";
                    }
                    else
                    {
                        for (int i = days_between_start; i < days_between_end; i++)
                        {
                            var param = new SqlParameter("@date_calender", DateTime.Now.AddDays(i).ToString("dd/MM/yyyy"));
                            context.Calenders.FromSql("add_calender", param);
                            context.SaveChanges();
                        }
                    }
                }
     
            }
            return View("Index");
        }

        public ActionResult Calender()
        {
            return View();
        }

    }
}