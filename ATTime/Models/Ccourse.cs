﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTime.Models
{
    public class Ccourse
    {
        public static string course(int? course)
        {
            ATTime_DBContext db = new ATTime_DBContext();
            var testtest = db.Courses
                .Where(s => s.CourseId == course)
                .Single().CourseName;
            return testtest;
        }
    }
}