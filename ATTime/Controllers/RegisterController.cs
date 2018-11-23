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
        public ActionResult CreateOperator(string firstname, string lastname, string username, string psw, string phone)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);

            using (var context = new ATTime_DBContext())
            {
                var oprtr = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = pasw,
                    Phone = phone,
                    RoleId = 1 
                };
                context.Operators.Add(oprtr);
                ViewBag.SuccessMessage = firstname + " was created";

                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                    return View("Operator");
                                    
                    
                } else
                {
                    return View("Index");
                }

            }
           
        }

    }
   
}