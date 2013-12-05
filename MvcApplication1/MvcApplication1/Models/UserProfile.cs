using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Listings = new List<Listing>();
            this.webpages_Roles = new List<webpages_Roles>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
