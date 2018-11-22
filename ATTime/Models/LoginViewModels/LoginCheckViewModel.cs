using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using ATTime.Models;

namespace ATTime.Models.LoginViewModels
{
    public class LoginCheckViewModel
    {
        public int CurrentId { get; set; }
        public string CurrentRole { get; set; }

        private List<LoginCheckViewModel> Currentuser = new List<LoginCheckViewModel>();

        public string Firstname()
        {
            var context = new ATTime_DBContext();
            var firstname = string.Empty;
            if (CurrentRole == "Student")
            {
                firstname = context.Students
                    .Where(s => s.StudentId == CurrentId)
                    .Single().FirstName;
            } else if (CurrentRole == "Teacher" || CurrentRole == "Admin")
            {
                firstname = context.Operators
                    .Where(s => s.OperatorId == CurrentId)
                    .Single().FirstName;
            }
            return firstname;
        }

        public string Lastname()
        {
            var context = new ATTime_DBContext();
            var lastname = string.Empty;
            if (CurrentRole == "Student")
            {
                lastname = context.Students
                    .Where(s => s.StudentId == CurrentId)
                    .Single().LastName;
            }
            else if (CurrentRole == "Teacher" || CurrentRole == "Admin")
            {
                lastname = context.Operators
                    .Where(s => s.OperatorId == CurrentId)
                    .Single().LastName;
            }
            return lastname;
        }

        public virtual void Adduser(int userid, string userrole)
        {
         Currentuser.Add(new LoginCheckViewModel { CurrentId = userid, CurrentRole = userrole });
        }
    }
}