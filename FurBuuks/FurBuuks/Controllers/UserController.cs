using FurBuuks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurBuuks.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            FurBuuksContext db = new FurBuuksContext();
            List<User> data = db.Users.ToList();
            return View(data);
        }
    }
}