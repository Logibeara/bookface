using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class Course
    {
        public int CourseID { get; set; }
        public string Dept { get; set; }
        public Nullable<int> CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string Section { get; set; }
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }
}
