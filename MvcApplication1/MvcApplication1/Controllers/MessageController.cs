using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        public ActionResult Message()
        {
            return View();
        }



        //
        // POST:

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AddTwo(int start)
        {
            //get model from db

            myInt model;
            using (  MessageContext db = new MessageContext())
            {
                model = db.Current.First<myInt>();
            }
           
            model.value += 2;
            //update w.o db
            return Content((model.value).ToString());
        }
    }
}
