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
            //int[] IDs = { 1, 2, 3, 4, 5, 6 };

            //List<Book> bookList = new List<Book>();
            //for(int i = 0; i < IDs.Length; i++)
            //{
            //    bookList.Add(BookUtils.getBook(IDs[i]));
            //}

            //ViewData["BookList"] = bookList;
            //ViewData["isListing"] = true;
            //return View("~/Views/Shared/BookList.cshtml");



            List<Book> bookList;
            using (TestDbContext db = new TestDbContext())
            {

                IQueryable<Book> books = from b in db.Books
         //                                      orderby b.BookName
                                               select b;

                bookList = books.ToList<Book>();
                foreach (var b in bookList)
                {
                    if (b != null)
                    {
                        //due to lazy loading, resolve all foreign key references
                        //listing.UserProfile = listing.UserProfile;
                        b.Course = b.Course;
                        b.Course.Books = b.Course.Books;
                    }
                    else
                    {
                        //no listing with the given ID, report error?
                    }
                }
            }


            ViewData["BookList"] = bookList;
            ViewData["isListing"] = true;

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
                    b.Author.StartsWith(term)).Select(b => new
                    {
                        value = b.BookName.Trim()
                    }).Take(5).ToArray();
               return this.Json(returnArr, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Search(int courseNum = 0, string bookName = "",
                                    string courseName = "", string author = "")
        {
        if (courseNum != 0)
            {
                ViewData["courseNum"] = courseNum;
            }
            if (!bookName.Equals(""))
            {
                ViewData["bookName"] = bookName;
            }
            if (!courseName.Equals(""))
            {
                ViewData["courseName"] = courseName;
            }
            if (!author.Equals(""))
            {
                ViewData["author"] = courseName;
            }
            return View("~/Views/Search/Search.cshtml");
        }

public ActionResult GetListingList(int courseNum = 0, string bookName = "",
                                    string courseName = "", string author = "")
        {
            using(TestDbContext db = new TestDbContext())
            {
                List<Listing> ListingList = db.Listings.Where(b => !bookName.Equals("") && b.Book.BookName.StartsWith(bookName) ||
                       !courseName.Equals("") && b.Book.Course.CourseName.StartsWith(courseName) ||
                       (courseNum == b.Book.Course.CourseNumber) ||
                      !author.Equals("")&& b.Book.Author.StartsWith(author)).ToList();
                for (int i = 0; i < ListingList.Count(); i++)
                {
                    ListingList.ElementAt(i).Book = ListingList.ElementAt(i).Book;
                }
                       ViewData["ListingList"] = ListingList;
                       ViewResult view = View("~/Views/Shared/ListingList.cshtml");
                       return view;
            }
        }

        //book details popup getter
        public ActionResult BookDetails(MvcApplication1.Models.Book clickedBook = null)//)
        {
            if (clickedBook == null)
            {
                return Content("error");
            }
            if (clickedBook.CourseID > 0)
            {
                using (TestDbContext db = new TestDbContext())
                {
                    clickedBook.Course = db.Courses.Find(clickedBook.CourseID);
                }
            }

            
            //get available listings for book

            Book resolvedBook = null;

            using (TestDbContext db = new TestDbContext())
            {

                int accountid = UserUtils.UserNametoID(User.Identity.Name);

                IQueryable<Book> books = from b in db.Books 
                                               where b.BookID == clickedBook.BookID
                                               select b;
                if (books.Count() > 0)
                {
                    resolvedBook = books.First();
                    //probably more loading here than needed
                    resolvedBook.Course = resolvedBook.Course;
                    resolvedBook.Course.Books = resolvedBook.Course.Books;
                    resolvedBook.Listings = resolvedBook.Listings;
                }
                
            }
            
            
            //pass back available listings
            ViewData["book"] = resolvedBook;
            return View("~/Views/Shared/BookDetails.cshtml");
        }
    }
}
