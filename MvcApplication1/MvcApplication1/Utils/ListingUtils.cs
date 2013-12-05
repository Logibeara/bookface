using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;

namespace MvcApplication1.Utils
{
    public static class ListingUtils
    {
        public static Listing getListing(int listingID)
        {
            Listing listing;

            using(TestDbContext db = new TestDbContext())
            {
                //book db depends on course db
                if (db.Courses.Count() == 0)
                {
                    CourseUtils.initCourseDb(db);
                }

                //initialize book table if needed
                if (db.Books.Count() == 0)
                {
                    BookUtils.initBookDb(db);
                }

                //initialize listing table if needed
                if (db.Listings.Count() == 0)
                {
                    initListingDb(db);
                }

                listing = db.Listings.Find(listingID);

                if (listing != null)
                {
                    //due to lazy loading, resolve all foreign key references
                    //listing.UserProfile = listing.UserProfile;
                    listing.Book = listing.Book;
                    listing.Book.Course = listing.Book.Course;
                }
                else
                {
                    //no listing with the given ID, report error?
                }
            }

            return listing;
        }

        public static void initListingDb(TestDbContext db)
        {
            db.Listings.Add(new Listing
            {
                UserID = UserUtils.UserNametoID("logibeara"),
                BookID = 1,
                Price = (decimal)10.00,
                ListType = 0,
                ListDate = System.DateTime.Now
            });

            db.Listings.Add(new Listing
            {
                UserID = 2,
                BookID = 5,
                Price = (decimal)5.00,
                ListType = 0,
                ListDate = System.DateTime.Now
            });

            db.Listings.Add(new Listing
            {
                UserID = 2,
                BookID = 3,
                Price = (decimal)30.00,
                ListType = 0,
                ListDate = System.DateTime.Now
            });

            db.Listings.Add(new Listing
            {
                UserID = 1,
                BookID = 3,
                Price = (decimal)30.00,
                ListType = 0,
                ListDate = System.DateTime.Now
            });

            db.Listings.Add(new Listing
            {
                UserID = 1,
                BookID = 2,
                Price = (decimal)15.00,
                ListType = 0,
                ListDate = System.DateTime.Now
            });
            db.SaveChanges();
        }
    }
}