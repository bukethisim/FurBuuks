using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurBuuks.Models
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public State State { get; set; }
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
    public enum State
    {
        Okundum,
        Okuyorum,
        Okuyacağım
    }
}