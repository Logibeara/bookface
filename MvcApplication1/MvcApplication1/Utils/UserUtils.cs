using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Utils
{
    public static class UserUtils
    {

        //
        // lookup accountID and return corresponding userName
        public static String UserIDtoName(int userId)
        {
            String userName;
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = (from u in db.UserProfiles
                                    where u.UserId == userId
                                    select u).First<UserProfile>();
            
                userName = user.UserName;
            }

            return userName;

        }

        //
        // lookup accountName and return corresponding userId
        public static int UserNametoID(String userName)
        {

            int id = 999;
            using (UsersContext db = new UsersContext())
            {
                IQueryable<UserProfile> users = (from u in db.UserProfiles
                                                where u.UserName.ToLower().Equals(userName.ToLower())
                                                select u);

                //id = user.UserId;
                id = users.First().UserId;
            }
            return id;
        }

    }
}
