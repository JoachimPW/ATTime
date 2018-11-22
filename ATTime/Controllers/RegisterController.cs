using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;

namespace ATTime.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOperator(string firstname, string lastname, string username, string psw, string phone)
        {
            using (var context = new ATTime_DBContext())
            {
                var oprtr = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = username,
                    Psw = psw,
                    Phone = phone,
                    RoleId = 1
                    

                };
                context.Operators.Add(oprtr);

                context.SaveChanges();
            }

            return Redirect("Index");
        }

    }
   
}