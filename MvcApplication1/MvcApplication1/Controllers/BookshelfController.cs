using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Utils;

namespace MvcApplication1.Controllers
{
    public class BookshelfController : Controller
    {
        //
        // GET: /Bookshelf/

        public ActionResult Bookshelf()
        {
            return View();
        }

        public ActionResult GetList()
        {
            int[] IDs = { 1, 2, 3, 4, 5 };

            List<Book> bookList = new List<Book>();
            for (int i = 0; i < IDs.Length; i++)
            {
                bookList.Add(BookUtils.getBook(IDs[i]));
            }

            ViewData["BookList"] = bookList;
            return View("~/Views/Shared/BookList.cshtml");
        }

        public ActionResult PopupTest(int id)
        {
            ViewData["id"] = id;
            return View("~/Views/Bookshelf/PopupTest.cshtml");
        }

        [HttpPost]
        public ActionResult AddBook(Listing toAdd, int userID)
        {
            String user = User.Identity.Name;
            if (user != null)
                {
                    //getUserID from logans utility
                    Listing listToAdd = new Listing();

                }
            return null;
        }

        [HttpGet]
        public ActionResult AddBookDialog()
        {
            return View("~/Views/Bookshelf/AddBook.cshtml");
        }

        //book details popup getter
        public ActionResult BookDetails(MvcApplication1.Models.Book clickedBook)
        {
            if (clickedBook.CourseID > 0)
            {
                using (TestDbContext db = new TestDbContext())
                {
                    clickedBook.Course = db.Courses.Find(clickedBook.CourseID);
                }
            }

            ViewData["book"] = clickedBook;
            return View("~/Views/Shared/BookDetails.cshtml");
        }
    }
}
