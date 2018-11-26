using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace ATTime.Controllers
{
    public class TestSearchController : Controller
    {

        ATTime_DBContext db = new ATTime_DBContext();
        // GET: TestSearch
        public ActionResult Index()
        {
            return View(db.Students.ToList());
            
        }
    }
}