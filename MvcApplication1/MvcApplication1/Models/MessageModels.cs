using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MvcApplication1.Models
{
    public class Message : DbContext
    {
        public Message()
            : base("UserDbConnect")
        {
        }

        public DbSet<myInt> Current { get; set; }
    }

    [Table("StartDb")]
    public class myInt
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int value { get; set; }
    }
}
