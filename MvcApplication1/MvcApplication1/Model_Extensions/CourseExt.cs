using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public partial class Course
    {
        //creates an abbreviation for the class, e.g. "ComS 319"
        public String abbrev()
        {
            return this.Dept + " " + this.CourseNumber;
        }
    }
}