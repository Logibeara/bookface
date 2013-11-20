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
                //book db depends on course db
                if (db.Courses.Count() == 0)
                {
                    CourseUtils.initCourseDb(db);
                }

                //initialize book table if needed
                if (db.Books.Count() == 0)
                {
                    initBookDb(db);
                }

                book = db.Books.Find(bookID);

                //This line is needed because LINQ requests are lazily loaded and the
                //TestDbContext was being destroyed before the Course was being resolved.
                //The same thing will need to be done for all foreign key lookups.
                book.Course = book.Course;

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
                CourseID = 1,
                Description = "She totally is.",
                ISBN = "123456789"
            });
            db.Books.Add(new Book
            {
                BookName = "The Mysterious Package",
                Author = "Mike Rotch",
                CourseID = 2,
                Description = "What could it contain?!",
                ISBN = "987654321"
            });
            db.Books.Add(new Book
            {
                BookName = "Saggy Diapers",
                Author = "Seymor Butts",
                CourseID = 2,
                Description = "Some things can't be unseen.",
                ISBN = "12345654321"
            });
            db.Books.Add(new Book
            {
                BookName = "The Man Inside Me",
                Author = "Tobias Funke",
                CourseID = 1,
                Description = "For there's a man inside me, and only when he's finally out, can I walk free of pain.",
                ISBN = "88888888888"
            });
            db.Books.Add(new Book
            {
                BookName = "The Neverending Search",
                Author = "Amanda Huganchis",
                CourseID = 2,
                Description = "Don't you want somebody to love?",
                ISBN = "1248163264"
            });
            db.Books.Add(new Book
            {
                BookName = "Whoopi",
                Author = "Goldberg",
                CourseID = 1,
                Description = "This book is awesome!",
                ISBN = "65322476513"
            });
            db.SaveChanges();
        }
    }
}