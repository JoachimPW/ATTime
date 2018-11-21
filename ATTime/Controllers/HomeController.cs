using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Data.Entity;


namespace ATTime.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var ctx = new ATTime_DBContext())
            {
                var team = ctx.Team.SqlQuery("Select team_name from team where team_name='WU-F18a'").FirstOrDefault<Student>();
            }
            return View();
        }

    }
}