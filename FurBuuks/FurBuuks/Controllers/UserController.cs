using FurBuuks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FurBuuks.Controllers
{
    public class UserController : Controller
    {
        FurBuuksContext db = new FurBuuksContext();
        // GET: User
        public ActionResult Index()
        {
            var u = ((User)Session["User"]);
            User user = db.Users.Find(u.Id);
            ViewBag.AllComments = (from c in db.Comments
                                   orderby c.CommentDate descending
                                   select c)
                                  .ToList();
            ViewBag.RecentBooks = db.Books
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToList();
            ViewBag.UserBooks = from ub in db.UserBooks
                                join b in db.Books
                                on ub.Book.Id equals b.Id
                                where ub.User.Id == u.Id
                                select b;
            return View(user);
        }

        public ActionResult UserProfile()
        {
            var u = ((User)Session["User"]);
            User user = db.Users.Find(u.Id);
            return View(user);
        }

        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult UserEdit(User edited, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var old = db.Users.Find(edited.Id);
                old.NameSurname = edited.NameSurname;
                old.Password = edited.Password;
                old.UserName = edited.UserName;
                old.Bio = edited.Bio;

                old.Email = edited.Email;
                old.FacebookURL = edited.FacebookURL;
                old.InstagramURL = edited.InstagramURL;
                old.TumblrURL = edited.TumblrURL;
                old.TwitterURL = edited.TwitterURL;
                old.GooglePlusURL = edited.GooglePlusURL;

                if (ImageURL != null)
                {
                    var path = Server.MapPath("/Uploads/UserProfilePicture/");
                    var filename = edited.Id + ".jpg";
                    ImageURL.SaveAs(path + filename);
                    old.ImageURL = "/Uploads/UserProfilePicture/" + filename;
                }


                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                Session["User"] = old;
                return RedirectToAction("UserProfile");
            }

            return View(edited);
        }

        [HttpGet]
        public ActionResult MyBooks()
        {
            User user = (User)Session["User"];
            return View(db.Users.Find(user.Id));
        }

        [HttpGet]
        public ActionResult BookEdit()
        {
            var user = (User)Session["User"];
            int id = user.Id;
            return View(db.Users.Find(id));
        }

        public JsonResult EditBook(int edited, DateTime? BeginTime, DateTime? EndTime)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserBook old = db.UserBooks.Find(edited);
                    old.BeginTime = BeginTime;
                    old.EndTime = EndTime;
                    db.Entry(old).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                User user1 = (User)Session["User"];
                var user1Id = user1.Id;
                var user = db.Users.Find(user1Id);
                var d = db.UserBooks.Find(id);
                db.Entry(user).Collection("UserBooks").Load();
                user.UserBooks.Remove(d);
                db.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult DeleteComment(int id)
        {
            try
            {
                var d = db.Comments.Find(id);
                db.Comments.Remove(d);
                db.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}