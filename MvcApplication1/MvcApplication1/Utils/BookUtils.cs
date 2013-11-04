using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;

namespace MvcApplication1.Utils
{
    public static class BookUtils
    {
        public static Book getBook(int bookID)
        {
            Book book;

            using (TestDbContext db = new TestDbContext())
            {
                //initialize book table if needed
                if (db.Books.Count() == 0)
                {
                    initBookDb(db);
                }

                book = db.Books.Find(bookID);
                if (book == null)
                {
                    //no book with the given ID, report error?
                }
            }

            return book;
        }

        //test function for initializing database
        private static void initBookDb(TestDbContext db)
        {
            db.Books.Add(new Book
            {
                BookName = "I Swear I'm Not Crazy",
                Author = "Oprah Winfrey",
            });
            db.Books.Add(new Book
            {
                BookName = "The Mysterious Package",
                Author = "Mike Rotch",
            });
            db.Books.Add(new Book
            {
                BookName = "Saggy Diapers",
                Author = "Seymor Butts",
            });
            db.Books.Add(new Book
            {
                BookName = "The Man Inside Me",
                Author = "Tobias Funke",
            });
            db.Books.Add(new Book
            {
                BookName = "The Neverending Search",
                Author = "Amanda Huganchis",
            });
            db.SaveChanges();
        }
    }
}