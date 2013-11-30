using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Utils;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;

namespace MvcApplication1.Controllers
{
    public class BrowseController : Controller
    {
        //
        // GET: /Browse/

        public ActionResult Browse()
        {
            return View();
        }

        public ActionResult GetBookList()
        {
            //test list of IDs to populate the list based on test book table
            int[] IDs = { 1, 2, 3, 4, 5, 6 };

            List<Book> bookList = new List<Book>();
            for(int i = 0; i < IDs.Length; i++)
            {
                bookList.Add(BookUtils.getBook(IDs[i]));
            }

            ViewData["BookList"] = bookList;
            return View("~/Views/Shared/BookList.cshtml");
        }

        public object AutoComplete(string term)
        {
            using(TestDbContext db = new TestDbContext())
            {
                int courseNum;
                bool success;
                success = Int32.TryParse(term, out courseNum);
               var returnArr = db.Books.Where(b => b.BookName.StartsWith(term) ||
                   b.Course.CourseName.StartsWith(term) ||
                   (success && courseNum == b.Course.CourseNumber) ||
                   b.Author.StartsWith(term)).Select(b => new {
                   value = b.BookName.Trim()
                }).Take(5).ToArray();
               return this.Json(returnArr, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
