using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATTime.Models.LoginViewModels;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;


namespace ATTime.Controllers
{
    public class TeacherViewController : Controller
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
                else if (((string)Session["UserRole"]) == "Admin")
                {
                    return View("~/AdminView/Index");
                }
                else if (((string)Session["UserRole"]) == "Teacher")
                {
                    //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
                    var context = new ATTime_DBContext();
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    var school = ((int)Session["School"]);
                    var schoolname = context.Schools.Where(s => s.SchoolId == school).Single().SchoolName;
                    var schoollogo = context.Schools.Where(s => s.SchoolId == school).Single().Logo;
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    ViewData["Schoolname"] = schoolname;
                    ViewData["Logo"] = schoollogo;
                    //Tilføj koden her: 
                    var teams_operator = context.TeamCourseOperators
                        .Where(s => s.OperatorId == currentid)
                        .Include(s => s.Team)
                        .ToList();

                    ViewBag.TO = teams_operator;

                    //Koden skal slutte her
                    return View();
                }
                else
                {
                    string routeName = ControllerContext.RouteData.Values["Default"].ToString();
                    return View(routeName);
                }
            }
        }
    }
}