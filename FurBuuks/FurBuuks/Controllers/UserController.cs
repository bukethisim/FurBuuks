﻿using FurBuuks.Models;
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
             
                old.Email = edited.Email;
                old.FacebookURL = edited.FacebookURL;
                old.InstagramURL = edited.InstagramURL;
                old.TumblrURL = edited.TumblrURL;
                old.TwitterURL = edited.TwitterURL;
                old.GooglePlusURL = edited.GooglePlusURL;
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();
                Session["User"] = old;
                return RedirectToAction("UserProfile");
            }

            return View(edited);
        }
    }
}