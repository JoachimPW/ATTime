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
                else if (((string)Session["UserRole"]) == "Admin")
                {
                    return View("~/AdminView/Index");
                }
                else if (((string)Session["UserRole"]) == "Teacher")
                {
                    //Her fanger vi alle sessions som indeholder information for den bruger som er logget ind:
                    var currentid = ((int)Session["UserId"]);
                    var currentrole = ((string)Session["UserRole"]);
                    ViewData["id"] = currentid;
                    ViewData["Role"] = currentrole;
                    //Tilføj koden her: 

                    //Koden skal slutte her
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