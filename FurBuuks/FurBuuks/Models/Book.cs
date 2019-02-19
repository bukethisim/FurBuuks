using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurBuuks.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Author { get; set; }
        [MaxLength(1000)]
        public string ImageURL { get; set; }
        public virtual List<UserBook> UserBooks { get; set; }
    }
}