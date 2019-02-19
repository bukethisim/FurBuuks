using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FurBuuks.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string NameSurname { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        public string Password { get; set; }
        [MaxLength(1000)]
        public string ImageURL { get; set; }
        [MaxLength(500)]
        public string Bio { get; set; }
        public virtual List<UserBook> UserBooks { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        public string GooglePlusURL { get; set; }
        public string TwitterURL { get; set; }
        public string TumblrURL { get; set; }
        [Required]
        public string Email { get; set; }
    }
}