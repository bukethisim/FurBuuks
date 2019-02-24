using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FurBuuks.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string BookName { get; set; }
        [MaxLength(255)]
        public string Content { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}