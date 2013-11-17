using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Utils;

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
            int[] IDs = { 1, 2, 3, 4, 5 };

            List<Book> bookList = new List<Book>();
            for(int i = 0; i < IDs.Length; i++)
            {
                bookList.Add(BookUtils.getBook(IDs[i]));
            }

            ViewData["BookList"] = bookList;
            return View("~/Views/Shared/BookList.cshtml");
        }
    }
}
