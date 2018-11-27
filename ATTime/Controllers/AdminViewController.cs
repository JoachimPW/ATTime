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
        ATTime_DBContext db = new ATTime_DBContext();
        public ActionResult Index()
        {

            var schoolid = ((int)Session["School"]);
            var team = db.Teams.Where(s => s.SchoolId == schoolid);

            ViewBag.team = team;

            //Her tjekker vi, som vi har en session med et id i:
            if (Session["UserId"] == null)
            {
                //får default route
                string routeName = ControllerContext.RouteData.Values["Default"].ToString();
                return View(routeName);
            }
            else
            {
                //Her tjekker vi vores role, og sender en person til et andet view, hvis de ikke har den rigtige role:
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
                    var context = new ATTime_DBContext();
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    var school = ((int)Session["School"]);
                    
                    var schoolname = context.Schools.FromSql("select * from school where school_id=1").Single().SchoolName;
                    var schoollogo = context.Schools.FromSql("select * from school where school_id=1").Single().Logo;
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    ViewData["Schoolname"] = schoolname;
                    ViewData["Logo"] = schoollogo;
                    ViewData["schoolid"] = school;
                    //Tilføj koden her: 
                   

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

        public ActionResult AddCalender(DateTime start_date, DateTime end_date, int team_id)
        {
            //tiløjer session data, for når denne action bliver brugt
            var context = new ATTime_DBContext();
            var currentid = ((int)Session["UserId"]);
            var currentrole = ((string)Session["UserRole"]);
            var school = ((int)Session["School"]);
            var schoolname = context.Schools.FromSql("select * from school").Single().SchoolName;
            var schoollogo = context.Schools.FromSql("select * from school").Single().Logo;
            ViewData["id"] = currentid;
            ViewData["Role"] = currentrole;
            ViewData["Schoolname"] = schoolname;
            ViewData["Logo"] = schoollogo;

            //tilføjer data for datoen i dag
            var today = DateTime.Now;
            var datestring = start_date.ToString("dd/MM/yyyy");

            ViewBag.msg = "Add days to your calender";

            //tjekker her at vores værdier ikke er null
            if (start_date == null || end_date == null)
            {
                ViewBag.msg = "Add start and end date";
                return View("Calender");
            }
            else
            { 
                //Her tjekker vi om end dato er mindre end start dato
                int days_between_start = (start_date - today).Days;
                int days_between_end = (end_date - today).Days;

                if(days_between_end < days_between_start)
                {
                    ViewBag.msg = "End date has to be later than Start date";
                    return View("Calender");
                }
                else
                {
                    //Her tjekker vi, at datoerne ikke allerede er i databasen
                    var datecount = context.Calenders
                           .Where(s => s.CalenderName == datestring)
                           .Count();

                    if (datecount > 0)
                    {
                        ViewBag.msg = "Date already in calender";
                        return View("Calender");
                    }
                    else
                    {
                        //Her laver vi et forloop, som tilføjer alle de datoer, som man gerne vil have med i sin kalender
                        for (int i = days_between_start; i < days_between_end; i++)
                        {

                            var param = new SqlParameter("@date_calender", DateTime.Now.AddDays(i).ToString("dd/MM/yyyy"));
                            var param1 = new SqlParameter("@school", school);
                            var param2 = new SqlParameter("@TeamID", team_id);
                            context.Database.ExecuteSqlCommand("exec add_calender @date_calender, @school, @TeamID", param, param1, param2);
                            context.SaveChanges();
                        }
                        return View("Calender");
                    }
                }
     
            }
        }

        public ActionResult Calender()
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
                    var context = new ATTime_DBContext();
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    var school = ((int)Session["School"]);
                    var schoolname = context.Schools.FromSql("select * from school").Single().SchoolName;
                    var schoollogo = context.Schools.FromSql("select * from school").Single().Logo;
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    ViewData["Schoolname"] = schoolname;
                    ViewData["Logo"] = schoollogo;
                    //Tilføj koden her:                   

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

        public ActionResult Add()
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
                    var context = new ATTime_DBContext();
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    var school = ((int)Session["School"]);
                    var schoolname = context.Schools.FromSql("select * from school").Single().SchoolName;
                    var schoollogo = context.Schools.FromSql("select * from school").Single().Logo;
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    ViewData["Schoolname"] = schoolname;
                    ViewData["Logo"] = schoollogo;
                    //Tilføj koden her: 
                    

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

    }
}