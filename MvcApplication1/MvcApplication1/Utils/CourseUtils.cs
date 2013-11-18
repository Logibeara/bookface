using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;

namespace MvcApplication1.Utils
{
    public static class CourseUtils
    {
        public static void initCourseDb(TestDbContext db)
        {
            db.Courses.Add(new Course
            {
                Dept = "ComS",
                CourseNumber = 319,
                Instructor = "Basu",
                Semester = "F13"
            });

            db.Courses.Add(new Course
            {
                Dept = "Potions",
                CourseNumber = 101,
                Instructor = "Snape",
                Semester = "F02"
            });
            db.SaveChanges();
        }
    }
}