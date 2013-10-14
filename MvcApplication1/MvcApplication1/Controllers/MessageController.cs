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

        public ActionResult AddTwo(int start)
        {

            return Content((start + 2).ToString());
        }


        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddTwo(myInt model)
        {
            model.value += 2;
            //update w.o db
            return Content((model.value + 2).ToString());
        }
    }
}
