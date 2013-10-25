using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class Listing
    {
        public int ListID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public Nullable<decimal> BuyPrice { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<System.DateTime> ListDate { get; set; }
    }
}
