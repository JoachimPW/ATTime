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
        public static string Firstname(int CurrentId, string CurrentRole)
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

        public static string Lastname(int CurrentId, string CurrentRole)
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

        public static string check(int userid, string role)
        {
            if(userid == 0)
            {
                return "Login";
            }
            else
            {
                if(role == "Admin")
                {
                    return "AdminView";
                }
                else if(role == "Teacher")
                {
                    return "TeacherView";
                }
                else if (role == "Student")
                {
                    return "StudentView";
                }
                else
                {
                    return "Login";
                }
            }
            
        }
    }
}