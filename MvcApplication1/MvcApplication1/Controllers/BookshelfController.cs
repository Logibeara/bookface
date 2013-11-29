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

        [HttpGet]
        //[HttpPost]
        //public ActionResult AddBook(Listing toAdd, int userID)
        public ActionResult AddBook()
        {
            //UserProfile u = new UserProfile();
            //using (TestDbContext db = new TestDbContext())
            //{
            //    u = (from a in db.UserProfiles
            //         where a.UserId == userID
            //         select a).First();
            //    if (u != null)
            //    {
            //        Listing listToAdd = new Listing();

            //    }
            //}
            //return null;
            return View("~/Views/Bookshelf/AddBook.cshtml");
        }
    }
}
