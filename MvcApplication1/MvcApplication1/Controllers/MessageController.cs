﻿using System;
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

        //get recieved messages for specified user  
        //GET: /
        [HttpGet]
        public ActionResult GetReceivedMessages(int accountid)
        {
            List<Message> msgList;

            //get list of messages with account id as 
            using (TestDbContext db = new TestDbContext())
            {

                IQueryable<Message> messages = from m in db.Messages
                                               orderby m.SendDate
                                               where m.RecipientID == accountid
                                               select m;

              msgList = messages.ToList<Message>();

            }
            
            ViewData["Messages"] = msgList;
            
            return View("~/Views/Message/MessageList.cshtml");
        }

        //grab web form for sending a message  
        //GET: /
        [HttpGet]
        public ActionResult Compose()
        {
            return View("~/Views/Message/Compose.cshtml");
        }

        //send a message through a post  
        //GET: /
        [HttpPost]
        public ActionResult Send(string recipient = "no recipient", string sender = "no sender", String subject = "no subject", String message = "empty message")
        {
            //convert names to strings?

            //create a new message object

            //write message to database
            using (TestDbContext db = new TestDbContext())
            {
                db.Messages.Add(new Message{
                    IsRead = false,
                    Message1 = message,
                    MessageID = 100,
                    RecipientID = 996,
                    SendDate = DateTime.Now,
                    SenderID = 100,
                    SenderName = sender}
                    );

                db.SaveChanges();
            }

            //return View("~/Views/Message/Compose.cshtml");
            return Content("Message Sent");
        
        }

        [HttpPost]
        public ActionResult GetMessage(string id){

            Message myMessage;
            int mid = Convert.ToInt32(id);
            using (TestDbContext db = new TestDbContext())
            {

                IQueryable<Message> messages = from m in db.Messages
                                               orderby m.SendDate
                                               where m.MessageID == mid
                                               select m;

                myMessage = messages.ToList<Message>().First<Message>();

            }

            ViewData["currentMessage"] = myMessage;
            return View("~/Views/Message/ViewMessage.cshtml");
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
