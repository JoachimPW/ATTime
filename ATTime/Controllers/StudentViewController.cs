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
    public class StudentViewController : Controller
    {
        public ActionResult Index()
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

            string url = LoginCheckViewModel.check(currentid, currentrole);

            return RedirectToAction("Index", url);
        }
    }
}