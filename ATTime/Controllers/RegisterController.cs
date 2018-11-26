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
    public class RegisterController : Controller
    {
        ATTime_DBContext db = new ATTime_DBContext();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Student()
        {
            var schoolid = ((int)Session["School"]);
            var student = db.Students.Where(s => s.SchoolId == schoolid);                    

            ViewBag.student = student;
            
            var schoolName = db.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["schoolname"] = schoolName;
            var adminUsername = ((string)Session["adminName"]);
            var adminFirstname = db.Operators.Where(s => s.Username == adminUsername).Single().FirstName;
            var adminLastname = db.Operators.Where(s => s.Username == adminUsername).Single().LastName;
            ViewData["adminFirstname"] = adminFirstname;
            ViewData["adminLastname"] = adminLastname;
            
            return View();         
           
        }

        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.Operators.Where(x => x.Username == userdata).SingleOrDefault();
            if(SearchData!=null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        [HttpPost]        
        public ActionResult CreateOperator(string schoolname, string logo, string firstname, string lastname, string username, string psw, string phone)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var school = new School()
                {
                    SchoolName = schoolname,
                    Logo = logo
                };
                context.Schools.Add(school);
                int latestId = school.SchoolId;

                var oprtr = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,
                    Phone = phone,
                    RoleId = 1,
                    SchoolId = latestId
                };

                if (ModelState.IsValid)
                {
                    context.Operators.Add(oprtr);
                    ViewBag.SuccessMessage = "The admin: " + "<" + username + ">" + " and the school: " + "<" + schoolname + ">" + " have been created";                               
                    context.SaveChanges();
                }           
                
            }
            return View("Admin");
        }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public int? SchoolId { get; set; }

        [HttpPost]
        public ActionResult CreateStudent(string firstname, string lastname, string username, string psw)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var schoolid = ((int)Session["School"]);
               
                var student = new Student()
                
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,         
                    SchoolId = schoolid
            };

                if (ModelState.IsValid)
                {
                    context.Students.Add(student);
                    ViewBag.SuccessMessage2 = "The student: " + "<" + username + ">" + " has been created";
                    context.SaveChanges();
                }
                var schoolName = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
                ViewData["schoolname"] = schoolName;
            }
            return RedirectToAction("Student");
        }

    }
   
}