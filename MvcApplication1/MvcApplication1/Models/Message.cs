using System;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public partial class Message
    {
        public int MessageID { get; set; }
        public Nullable<int> RecipientID { get; set; }
        public Nullable<int> SenderID { get; set; }
        public string Message1 { get; set; }
        public Nullable<bool> IsRead { get; set; }
    }
}
