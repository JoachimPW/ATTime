using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATTime.Models.LoginViewModels;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;
using System.Text;

namespace ATTime.Controllers
{
    public class LoginController : Controller
    {
        private LoginCheckViewModel logincheckviewModel;

        public ActionResult Index(LoginModel model)
        {
            ViewBag.msg = "";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var username = model.Username;

                var pasw = string.Empty;
                byte[] encode = new byte[model.Password.Length];
                encode = Encoding.UTF8.GetBytes(model.Password);
                pasw = Convert.ToBase64String(encode);

                var password = pasw;

                var context = new ATTime_DBContext();
                var OperatorUsername = context.Operators
                            .Where(s => s.Username == username)
                            .Count();
                var StudentUsername = context.Students
                          .Where(s => s.Username == username)
                          .Count();
                if(OperatorUsername > 0)
                {
                    var operatormatch = context.Operators
                            .Where(s => s.Username == username)
                            .Single().Psw;
                    if(operatormatch == password)
                    {
                        var operatorid = context.Operators
                            .Where(s => s.Username == username)
                            .Single().OperatorId;
                        var operatorrole = context.Operators
                           .Where(s => s.Username == username)
                           .Include(s => s.Role)
                           .Single().Role.RoleName;
                        Session["UserId"] = operatorid;
                        Session["UserRole"] = operatorrole;
                        if(operatorrole == "Admin")
                        {
                            return Redirect("~/AdminView/Index");
                        }
                        else if (operatorrole == "Teacher")
                        {
                            return Redirect("~/TeacherView/Index");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Wrong password";
                    }
                }
                else if (StudentUsername > 0)
                {
                    var studentmatch = context.Students
                            .Where(s => s.Username == username)
                            .Single().Psw;
                    if (studentmatch == password)
                    {
                        string strundentrole = "Student";
                        var studentid = context.Students
                            .Where(s => s.Username == username)
                            .Single().StudentId;
                        Session["UserId"] = studentid;
                        Session["UserRole"] = strundentrole;
                        return Redirect("~/StudentView/Index");
                    }
                    else
                    {
                        ViewBag.msg = "Wrong password";
                    }
                }
                else
                {
                    ViewBag.msg = "User doesn't exist";
                }
            }
            else
            {
                ViewBag.msg = "Please enter username and password";
            }
            return View("Index");
        }
    }
}