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
            //TODO get user's listings instead of hardcoded listing IDs
            int[] IDs = { 1, 2, 3, 4, 5 };

            List<Listing> list = new List<Listing>();
            for (int i = 0; i < IDs.Length; i++)
            {
                list.Add(ListingUtils.getListing(IDs[i]));
            }

            ViewData["ListingList"] = list;
            return View("~/Views/Shared/ListingList.cshtml");
        }

        public ActionResult PopupTest(int id)
        {
            ViewData["id"] = id;
            return View("~/Views/Bookshelf/PopupTest.cshtml");
        }

        [HttpPost]
        public ActionResult AddBook(String bookName = "none", String iSBN = "none", String author = "none", String price = "$0.00")
        {
            String user = User.Identity.Name;
            if (user != null)
            {
                //getUserID from logans utility
                using (TestDbContext db = new TestDbContext())
                {
                    Book bookToCheck = new Book();
                    bookToCheck.BookName = bookName;
                    bookToCheck.ISBN = iSBN;
                    bookToCheck.Author = author;
                    //TODO set the fields of book accoring to the data in the dialog
                    Listing listToAdd = new Listing();
                    //adduser to listing
                    //see if book exists in database
                    List<Book> bookList = (List<Book>)db.Books.Where(b => b.BookName.Equals(bookToCheck.BookName) &&
               b.Author.Equals(bookToCheck.Author));
                    if (bookList.Count > 0)
                    {
                        Book bookChecked = bookList.First();
                        if (bookChecked != null && bookChecked.BookID != null)
                        {
                            listToAdd.BookID = bookChecked.BookID;
                        }
                    }
                    else
                    {
                        db.Books.Add(bookToCheck);
                        db.SaveChanges();
                        Book bookChecked = db.Books.Where(b => b.BookName.Equals(bookToCheck.BookName) &&
               b.Author.Equals(bookToCheck.Author)).First();
                        listToAdd.BookID = bookChecked.BookID;
                    }
                    //ifnot add the book to the database
                    //add the bookID to the Listing
                    //add the Listing date to the Listing object
                    listToAdd.ListDate = DateTime.Now;
                    //set list type to 0
                    listToAdd.ListType = 0;
                    //add the liting to the database
                    db.Listings.Add(listToAdd);
                    db.SaveChanges();
                }
            }
            return Content("Book Added");
        }

        [HttpGet]
        public ActionResult AddBookDialog()
        {
            return View("~/Views/Bookshelf/AddBook.cshtml");
        }

        //listing details popup getter
        public ActionResult ListingDetails(string seller, decimal? price, MvcApplication1.Models.Book clickedBook)
        {
            if (clickedBook.CourseID > 0)
            {
                using (TestDbContext db = new TestDbContext())
                {
                    clickedBook.Course = db.Courses.Find(clickedBook.CourseID);
                }
            }

            ViewData["seller"] = (seller == null) ? "" : seller;
            ViewData["price"] = (price == null) ? 0m : price;
            ViewData["book"] = clickedBook;
            return View("~/Views/Shared/ListingDetails.cshtml");
        }
    }
}
