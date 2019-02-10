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
            var user = db.Users.FirstOrDefault();
            ViewBag.UserPhoto = user.ImageURL;
            return View(user);
        }

        public ActionResult UserProfile()
        {
            var user = ((User)Session["User"]);
            return View(user);
        }  

        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult UserEdit(User edited)
        {
            if(ModelState.IsValid)
            {
                var old = db.Users.Find(edited.Id);
                old.NameSurname = edited.NameSurname;
                old.Password = edited.Password;
                old.UserName = edited.UserName;
                old.Bio = edited.Bio;
                //old.SMLinks.Email = edited.SMLinks.Email;
                //old.SMLinks.FacebookURL = edited.SMLinks.FacebookURL;
                //old.SMLinks.InstagramURL = edited.SMLinks.InstagramURL;
                //old.SMLinks.TumblrURL = edited.SMLinks.TumblrURL;
                //old.SMLinks.TwitterURL = edited.SMLinks.TwitterURL;
                //old.SMLinks.GooglePlusURL = edited.SMLinks.GooglePlusURL;
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("UserProfile");
            }

            return View(edited);
        }
    }
}