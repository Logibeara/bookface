using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class Book
    {
        public Book()
        {
            this.Listings = new List<Listing>();
        }

        public int BookID { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public byte[] CoverPic { get; set; }
        public Nullable<int> CourseID { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
    }
}
