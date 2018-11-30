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

        public ActionResult Team()
        {
            var schoolid = ((int)Session["School"]);
            var team = db.Teams.Where(s => s.SchoolId == schoolid);
            ViewBag.team = team;
            return View();
        }
        [HttpPost]
        public ActionResult SelectTeam(int teamid)
        {
            ViewBag.testTeamId = teamid;
            Session["TeamId"] = teamid;
            return RedirectToAction("Student");
        }

        public ActionResult Student()
        {
            var teamId = ((int)Session["TeamId"]);
            ViewBag.teamid = teamId;
            ViewData["TEAMID"] = teamId;

            var schoolid = ((int)Session["School"]);

            var test = db.TeamStudents.Where(t => t.TeamId == teamId).Include(t => t.Student).ToList();
            var test2 = db.TeamOperators.Where(t => t.TeamId == teamId).Include(t => t.Operator).ToList();

            // var test1 = db.TeamStudents.Where(t => test.Contains(t.));

            var courseName = db.CourseStudents.Where(t => t.CourseId == t.Course.CourseId).Include(t => t.Course).ToList();

            //var CourseName2 = db.CourseStudents.Where(r => test.Contains(r.StudentId));

            var hej = db.TeamStudents.Where(t => t.TeamId == teamId).Include(a => a.Student).ThenInclude(b => b.CourseStudent).ToList();

            foreach(CourseStudent item in courseName)
            {
                var matches = test.Where(row => item.StudentId == row.StudentId);
                ViewBag.komnu = matches;
            }

            ViewBag.CourseNAME = courseName;
            var studentliste = db.Students.Where(s => s.SchoolId == schoolid);
            ViewBag.studentListe = studentliste;
            ViewBag.tester = test;

            ViewBag.tester2 = test2;

            // Teachers // 
            
            var oprtr = db.Operators.Where(s => s.SchoolId == schoolid).Where(i => i.RoleId == 2);
            ViewBag.oprtr = oprtr;

            var courses = db.Courses.ToList();
            ViewBag.CourseNAME = courses;

            /* if(tcs > 1)
             { 
             var student = db.Students.Where(s => s.StudentId == tcs);
             ViewBag.studentListe = student;
             } else
             {
            var student = db.Students.Where(s => s.SchoolId == schoolid);
                ViewBag.studentListe = student;
                foreach (TeamCourseStudent s in students)
            {
                var student = db.Students.Where(a => a.SchoolId == schoolid);
            }
                */
            //var studentsInTable = db.Teams.Where(c => c.TeamId == teamId).SelectMany(c => c.TeamCourseStudent);


            //var zzzz = db.TeamCourseStudents.Where(c => c.TeamId == teamId).SelectMany(c => c.Student);
            //  /db.TeamCourseStudents.Where(s => s.TeamId == teamId).Where(db.Students.Where());
            // ViewBag.student = studenterliste;

            var schoolName = db.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
            ViewData["schoolname"] = schoolName;
            var adminUsername = ((string)Session["adminName"]);
            var adminFirstname = db.Operators.Where(s => s.Username == adminUsername).Single().FirstName;
            var adminLastname = db.Operators.Where(s => s.Username == adminUsername).Single().LastName;
            ViewData["adminFirstname"] = adminFirstname;
            ViewData["adminLastname"] = adminLastname;

            var studentList = db.Students.ToList();
            ViewBag.STUDENT = studentList;
            
            return View();
        }

        public ActionResult Teacher()
        {
            var currentid = ((int)Session["UserId"]);
            var schoolid = ((int)Session["School"]);
            var oprtr = db.Operators.Where(s => s.SchoolId == schoolid).Where(i => i.RoleId == 2) ;
            ViewBag.oprtr = oprtr;

            var test2 = db.TeamOperators.Include(t => t.Operator).ToList();

            ViewBag.tester2 = test2;

            var courses = db.Courses.ToList();
            ViewBag.CourseNAME = courses;

            return View();
        }
        /*[HttpPost]
        public ActionResult _SearchStudent()
        {
            return View(db.Students.ToList());
        } */

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

        public JsonResult CheckTeamNameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = db.Teams.Where(x => x.TeamName == userdata).SingleOrDefault();
            if (SearchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        [HttpPost]
        public ActionResult CreateTeacher(string firstname, string lastname, string username, string psw, string phone, int courseid)
        {
            var pasw = string.Empty;
            byte[] encode = new byte[psw.Length];
            encode = Encoding.UTF8.GetBytes(psw);
            pasw = Convert.ToBase64String(encode);
            var schoolid = ((int)Session["School"]);
            
            // Til at generere username ud fra navn + 2 random tal //
            string firstChars = firstname.Substring(0, 2);
            string lastChars = lastname.Substring(0, 3);
            var allChar = "0123456789";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 2)
               .Select(token => token[random.Next(token.Length)]).ToArray());
            string rndmNumbers = resultToken.ToString();
            string combineChars = firstChars + lastChars + rndmNumbers;

            using (var context = new ATTime_DBContext())
            {               
                var teacher = new Operator()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = combineChars,
                    Psw = pasw,
                    Phone = phone,
                    RoleId = 2,
                    SchoolId = schoolid
                };

                // Adder teacher til databasen før vi kan tilføje FK til team_operator
                context.Operators.Add(teacher);
                // Grabber operatorID fra den nylig tilføjede teacher
                int latestTeacherId = teacher.OperatorId;               

                var teamOperator = new TeamOperator()
                {
                    OperatorId = latestTeacherId
                };

                var courseOperator = new CourseOperator()
                {
                    CourseId = courseid,
                    OperatorId = latestTeacherId
                };

                if (ModelState.IsValid)
                {
                    context.TeamOperators.Add(teamOperator);
                    context.CourseOperators.Add(courseOperator);
                    ViewBag.SuccessMessage = "The teacher: " + "<" + username + ">" + " has been created";
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Teacher");
        }

        [HttpPost]
        public ActionResult UpdateTeacher(int operatorid)
        {
            var teamId = ((int)Session["TeamId"]);
            var updatedTeacher = db.TeamOperators.Where(i => i.OperatorId == operatorid).First();
            updatedTeacher.TeamId = teamId;

            db.SaveChanges();

            return RedirectToAction("Student");
        }

        [HttpPost]
        public ActionResult AddCourseStudent(int studentid, int courseId)
        {
            var updatedStudent = db.CourseStudents.Where(i => i.StudentId == studentid).First();

            if (updatedStudent.CourseId == courseId)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var studentAddedCourse = new CourseStudent()
                {
                    StudentId = studentid,
                    CourseId = courseId
                };
                db.CourseStudents.Add(studentAddedCourse);
                db.SaveChanges();
            }  
         
            return RedirectToAction("Student");
        }

        [HttpPost]        
        public ActionResult CreateOperator(string schoolname, string logo, string firstname, string lastname, string username, string psw, string phone)
        {

            var getCourses = db.Courses.ToList();
            SelectList studentLIST = new SelectList(getCourses, "course_id", "course_name");
            ViewBag.DROPDOWNCOURSES = studentLIST;

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
                var teamId = ((int)Session["TeamId"]);

                // Genererer username ud fra de to første bogstaver i fornavn, tre første i efternavn og så to random cifre //
                string firstChars = firstname.Substring(0, 2);
                string lastChars = lastname.Substring(0, 3);
                var allChar = "0123456789";
                var random = new Random();
                var resultToken = new string(
                   Enumerable.Repeat(allChar, 2)
                   .Select(token => token[random.Next(token.Length)]).ToArray());
                string rndmNumbers = resultToken.ToString();
                string combineChars = firstChars + lastChars + rndmNumbers;
                
                var student = new Student()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Username = combineChars,
                    Psw = pasw,
                    SchoolId = schoolid,
                    
                };

                context.Students.Add(student);
                int latestStudentId = student.StudentId;
                var teamStudent = new TeamStudent()
                {
                    TeamId = teamId,
                    StudentId = latestStudentId
                };

                var courseStudent = new CourseStudent()
                {
                    CourseId = 2,
                    StudentId = latestStudentId
                };

                if (ModelState.IsValid)
                {
                    context.TeamStudents.Add(teamStudent);
                    context.CourseStudents.Add(courseStudent);
                    ViewBag.SuccessMessage2 = "The student: " + "<" + username + ">" + " has been created";
                    context.SaveChanges();
                }
                var schoolName = context.Schools.Where(s => s.SchoolId == schoolid).Single().SchoolName;
                ViewData["schoolname"] = schoolName;          

            }
            return RedirectToAction("Student");
        }
        
        public ActionResult CreateTeam(string teamname)

        {
            using (var context = new ATTime_DBContext())
            {
                var schoolid = ((int)Session["School"]);

                //Tilfæjer det nye team, som bliver oprettet.
                var team = new Team()
                {
                    TeamName = teamname,
                    SchoolId = schoolid
                };
                context.Teams.Add(team);
              
                //Skaffer data, som skal bruges til at få indsat id'er i junction tabel for fag for et team
                int teamid = team.TeamId;
                var today = DateTime.Now.ToString("dd/MM/yyyy");
                var today_id = db.Calenders.Where(s => s.CalenderName == today).Single().CalenderId;
                var forward = db.Calenders.Where(s => s.CalenderId > today_id).ToList();

                //tjekker om modellen er valid, og hvis den er valid, 
                //så bliver der tilføjet det nye team, samt id'er i junction tabellet.
                if (ModelState.IsValid)
                {
                    context.Teams.Add(team);
                    foreach (Calender c in forward)
                    {
                        var check = db.CourseCalenders.Where(d => d.TeamId == teamid).Count();
                        if (check > 0)
                        {

                        }
                        else
                        {
                            context.CourseCalenders.Add(new CourseCalender() { CourseId = 1, CalenderId = c.CalenderId, SchoolId = schoolid, TeamId = teamid });
                        }
                    }
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Team");
        }
        /* SØGEFUNKTION  

        [HttpPost]
        public ActionResult Index(string firstname)
        {
            ATTime_DBContext db = new ATTime_DBContext();
            var firstnamelist = db.Students.FromSql("select * from student where first_name=@p0", firstname).ToList();
            return View(firstnamelist);
        }*/


        [HttpPost]
        public ActionResult DeleteStudent(int studentid)
        {

            if (studentid == 0)
            {
                ViewData["msg"] = "Id not found";
            }
            using (var context = new ATTime_DBContext())
            {
                var students = context.Students.FirstOrDefault(s => s.StudentId == studentid);
                var teamStudents = context.TeamStudents.Single(s => s.StudentId == studentid);
                var courseStudents = context.CourseStudents.Single(s => s.StudentId == studentid);
                if (students == null)
                {
                    ViewData["msg"] = "Student not found";
                }
                context.CourseStudents.Remove(courseStudents);
                context.TeamStudents.Remove(teamStudents);
                context.Students.Remove(students);

                context.SaveChanges();
            }
            return RedirectToAction("Student", "Register");
        }

        [HttpPost]
        public ActionResult CreateCourse(string coursename)
        {            
            try { 
                var NewCourse = new Course()
                {
                    CourseName = coursename
                };

            db.Courses.Add(NewCourse);

            db.SaveChanges();
            }

            catch
            {

            }
            return RedirectToAction("Team");
        }



    }
   
}