using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;

namespace ATTime.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new ATTime_DBContext();

            //var teamname = context.Team
            //                         .Where(s => s.TeamId == 2)
            //                       .FirstOrDefault();

            var teamname = context.Team
                        .FromSql("Select team_name, team_ID from team")
                        .ToList();

            ViewBag.team = teamname;
            ViewData["hej"] = teamname;
            return View();
        }

    }
}