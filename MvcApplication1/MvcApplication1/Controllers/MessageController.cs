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

        [HttpGet]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AddTwo(int start)
        {
            //get model from db

            int retVal = 0;
            using (  TestDbContext db = new TestDbContext())
            {
                var result = (from b in db.StartDbs
                             select b).FirstOrDefault();
                if (result == null)
                {
                    db.StartDbs.Add(new StartDb { Id = 0, value = 0 });
                    db.SaveChanges();

                }
                else
                {
                    var finalResult = (result as StartDb).value;

                    finalResult += 2;
                    //write back

                    result.value = finalResult;
                    retVal = finalResult;
                    db.SaveChanges();
                }
            }
           
            
            return Content((retVal).ToString());
        }
    }
}
