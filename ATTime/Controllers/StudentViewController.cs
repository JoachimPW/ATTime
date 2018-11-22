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
        private LoginCheckViewModel logincheckviewModel;

        public StudentViewController(LoginCheckViewModel logincheck)
        {
            logincheckviewModel = logincheck;
        }

        public ViewResult Index()
        {
            ViewBag.Name = logincheckviewModel.Firstname() + logincheckviewModel.Lastname();
            if (logincheckviewModel == null)
            {
                return View("~/Views/StudentView/Index.cshtml");

            } 

            return View();
        }
    }
}