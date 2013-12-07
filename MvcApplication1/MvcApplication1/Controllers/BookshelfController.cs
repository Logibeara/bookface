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
        const String Ipsum = "Lorem ipsum dolor sit amet, quaeque nominati in ius, eum ut fastidii vivendum tincidunt. Ea eos discere corpora senserit.";
        //
        // GET: /Bookshelf/

        public ActionResult Bookshelf()
        {
            return View();
        }
        public ActionResult Wishlist()
        {
            return View("~/Views/Bookshelf/Wishlist.cshtml");
        }
        public ActionResult GetBookshelf()
        {
            /*
            //TODO get user's listings instead of hardcoded listing IDs
            int[] IDs = { 1, 2, 3, 4, 5 };

            List<Listing> list = new List<Listing>();
            for (int i = 0; i < IDs.Length; i++)
            {
                list.Add(ListingUtils.getListing(IDs[i]));
            }
            */
            List<Listing> listingList;

            using (TestDbContext db = new TestDbContext())
            {
                //scrub db of all expired listings before fetching
                removeExpiredListings(db);

                int accountid = UserUtils.UserNametoID(User.Identity.Name);

                IQueryable<Listing> listings = from l in db.Listings
                                               orderby l.ListDate descending
                                               where l.UserID == accountid
                                               where l.ListType == 0 //0 is books to sell
                                               select l;

                listingList = listings.ToList<Listing>();
                foreach (var l in listingList)
                {
                    if (l != null)
                    {
                        //due to lazy loading, resolve all foreign key references
                        //listing.UserProfile = listing.UserProfile;
                        l.Book = l.Book;
                        l.Book.Course = l.Book.Course;
                    }
                    else
                    {
                        //no listing with the given ID, report error?
                    }
                }
            }


            ViewData["ListingList"] = listingList;

            return View("~/Views/Shared/ListingList.cshtml");
        }

        public ActionResult GetWishlist()
        {
            List<Listing> listingList;
            using (TestDbContext db = new TestDbContext())
            {

                int accountid = UserUtils.UserNametoID(User.Identity.Name);

                IQueryable<Listing> listings = from l in db.Listings
                                               orderby l.ListDate descending
                                               where l.UserID == accountid
                                               where l.ListType == 1 //1 is books to buy
                                               select l;

                listingList = listings.ToList<Listing>();
                foreach (var l in listingList)
                {
                    if (l != null)
                    {
                        //due to lazy loading, resolve all foreign key references
                        //listing.UserProfile = listing.UserProfile;
                        l.Book = l.Book;
                        l.Book.Course = l.Book.Course;
                    }
                    else
                    {
                        //no listing with the given ID, report error?
                    }
                }
            }
            ViewData["ListingList"] = listingList;
            return View("~/Views/Shared/ListingList.cshtml");
        }

        public ActionResult PopupTest(int id)
        {
            ViewData["id"] = id;
            return View("~/Views/Bookshelf/PopupTest.cshtml");
        }

        public ActionResult AddListing(String bookName = "none", String author = "none", String description = "none", String course = "none", String iSBN = "none", Decimal price = 0.00m, int listingType = -1)
        {
            if (listingType == -1) return Content("Book Add Failed");
            String user = User.Identity.Name;
            if (user != null)
            {
                //getUserID from logans utility
                using (TestDbContext db = new TestDbContext())
                {
                    Book bookToCheck = new Book();
                    bookToCheck.BookName = bookName;
                    bookToCheck.Author = author;
                    bookToCheck.Description = Ipsum;
                    bookToCheck.ISBN = iSBN;

                    //TODO set the fields of book accoring to the data in the dialog
                    Listing listToAdd = new Listing();

                    listToAdd.UserID = UserUtils.UserNametoID(user);
                    //adduser to listing
                    //see if book exists in database
                    //Book bookChecked = db.Books.Where(b => b.BookName.Equals(bookToCheck.BookName) && b.Author.Equals(bookToCheck.Author)).First();
                    var myBook = (from b in db.Books
                                  where b.BookName == bookName
                                  where b.Author == author
                                  select b).ToList();
                    if (myBook.Count > 0)
                    {
                        if (myBook[0] != null && myBook[0].BookID != null)
                        {
                            listToAdd.BookID = myBook[0].BookID;
                        }
                    }
                    else
                    {
                        Course tempCourse = new Course();
                        bookToCheck.Course = tempCourse;
                        tempCourse.CourseName = "not implimented";
                        tempCourse.CourseNumber = 123;
                        var courseFromDB = (from c in db.Courses
                                           where c.CourseName == tempCourse.CourseName
                                           where c.CourseNumber == tempCourse.CourseNumber
                                           select c).ToList();
                        if (courseFromDB.Count > 0)
                        {
                            bookToCheck.Course = courseFromDB[0];
                            bookToCheck.CourseID = courseFromDB[0].CourseID;
                        }
                        else
                        {
                            bookToCheck.Course = tempCourse;
                        }

                        db.Books.Add(bookToCheck);
                        db.SaveChanges();
                        myBook = (from b in db.Books
                                  where b.BookName == bookName
                                  where b.Author == author
                                  select b).ToList();
                        listToAdd.BookID = myBook[0].BookID;
                    }
                    //ifnot add the book to the database
                    //add the bookID to the Listing
                    //add the Listing date to the Listing object
                    listToAdd.ListDate = DateTime.Now;
                    listToAdd.Price = Convert.ToDecimal(price);
                    //set list type to what ever it needs to be
                    listToAdd.ListType = listingType;
                    //add the liting to the database
                    db.Listings.Add(listToAdd);
                    db.SaveChanges();
                }
            }
            return Content("Book Added");
        }

        [HttpPost]
        public ActionResult RemoveListing(int listId)
        {
            using (TestDbContext db = new TestDbContext())
            {
                var listToRemove = db.Listings.Find(listId);
                if (listToRemove != null)
                {
                    db.Listings.Remove(listToRemove);
                    db.SaveChanges();
                    return Content("Listing Removed");
                }
                else
                {
                    return Content("Remove Failed");
                }
            }
        }

        [HttpPost]
        public ActionResult AddBook(String bookName = "none", String author = "none", String description = "none", String course = "none", String iSBN = "none", decimal price = 0.00m)
        {
            return AddListing(bookName, author, description, course, iSBN, price, 0);
        }

        [HttpPost]
        public ActionResult AddBookToWishlist(String bookName = "none", String author = "none", String description = "none", String course = "none", String iSBN = "none", decimal price = 0.00m)
        {
            return AddListing(bookName, author, description, course, iSBN, price, 1);
        }

        [HttpGet]
        public ActionResult AddBookDialog()
        {
            return View("~/Views/Bookshelf/AddBook.cshtml");
        }

        [HttpGet]
        public ActionResult AddBookToWishlistDialog()
        {
            return View("~/Views/Bookshelf/AddBookToWishlist.cshtml");
        }

        //listing details popup getter
        public ActionResult ListingDetails(string seller, decimal? price, MvcApplication1.Models.Book clickedBook, int listID)
        {
            //if (clickedBook.CourseID > 0)
            //{
            //    using (TestDbContext db = new TestDbContext())
            //    {
            //        clickedBook.Course = db.Courses.Find(clickedBook.CourseID);
            //    }
            //}

            //currently seller is being passed as an ID, this will probably change


            //ViewData["seller"] = (seller == null) ? "" : seller;
            ViewData["seller"] = UserUtils.UserIDtoName(Convert.ToInt32(seller));
            ViewData["price"] = (price == null) ? 0m : price;
            ViewData["listID"] = listID;
            Book resolvedBook = null;
            using (TestDbContext db = new TestDbContext())
            {
                var books = (from b in db.Books
                             where b.BookID == clickedBook.BookID
                             select b).ToList();
                if (books.Count() > 0)
                {
                    resolvedBook = books[0];

                    //LAZY LOAD COURSE
                    resolvedBook.Course = resolvedBook.Course;
                }
            }
            ViewData["book"] = resolvedBook;
            return View("~/Views/Shared/ListingDetails.cshtml");
        }

        private void removeExpiredListings(TestDbContext db)
        {
            var expiredListings = (from l in db.Listings
                                   where System.Data.Objects.EntityFunctions.DiffDays(l.ListDate, DateTime.Now) > 90
                                   select l).ToList();

            foreach (Listing listing in expiredListings)
            {
                db.Listings.Remove(listing);
            }

            db.SaveChanges();
        }
    }
}
