using FurBuuks.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FurBuuks.Controllers
{
    public class UserController : Controller
    {
        FurBuuksContext db = new FurBuuksContext();
        // GET: User
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(); /*((User)Session["User"]);*/
            ViewBag.UserPhoto = user.ImageURL;
            return View(user);
        }

        public ActionResult UserProfile()
        {
            var user = ((User)Session["User"]);
            return View(user);
        }  

        public ActionResult UserEdit()
        {
            return View();
        }
    }
}