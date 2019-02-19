using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FurBuuks.Models
{
    public class FurBuuksContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}