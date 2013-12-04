using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class Listing
    {
        public int ListID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int ListType { get; set; }
        public Nullable<System.DateTime> ListDate { get; set; }
        public virtual Book Book { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
