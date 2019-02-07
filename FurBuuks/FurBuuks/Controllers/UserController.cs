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
            var user = db.Users.FirstOrDefault();
            ViewBag.UserPhoto = user.ImageURL;
            return View(user);
        }

        public ActionResult UserProfile()
        {
            FurBuuksContext db = new FurBuuksContext();
            List<User> data = db.Users.ToList();
            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            FurBuuksContext db = new FurBuuksContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameSurname,UserName,Password,ImageURL,SMLinks,Bio")] User user)
        {
            FurBuuksContext db = new FurBuuksContext();
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}