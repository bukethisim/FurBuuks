﻿using FurBuuks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurBuuks.Controllers
{
    public class LibraryController : Controller
    {
        FurBuuksContext db = new FurBuuksContext();
        // GET: Library
        public ActionResult Index(int? page)
        {
            var user =(User) Session["User"];
            ViewBag.User = user.Id;
            List<Book> list = new List<Book>();
            if (page.HasValue)
            {
                int step = (page.Value - 1) * 4; //nullable ise .value alarak işlem yaparız.
                list = db.Books.OrderBy(x => x.Id).Skip(step).Take(4).ToList();
            }
            else
            {
                list = db.Books.Take(4).ToList();
            }

            float BookCount = db.Books.Count();
            double PageCount = Math.Ceiling(BookCount / 4);
            int current = page.HasValue ? page.Value : 1;

            ViewBag.Start = current > 2 ? current - 2 : 1;
            ViewBag.End = current < PageCount - 2 ? current + 2 : PageCount;
            ViewBag.CurrentPage = current;

            ViewBag.PrevVisible = current > 1;
            ViewBag.NextVisible = current < PageCount;
            return View(list);
        }

        public ActionResult AddToBook(int id)
        {
            User user = (User)Session["User"];

            if (user.UserBooks == null)
                user.UserBooks = new List<UserBook>();
            
            if(user.UserBooks.Any(x=> x.Book.Id == id))
            {
                ViewBag.Error = "Bu kitap zaten kitaplığınız da mevcut.";
                return RedirectToAction("Index", "Library");
            }
            else
            {
                var u = db.Users.Find(user.Id);
                Book choosenBook = db.Books.Find(id);
                UserBook newBook = new UserBook();
                newBook.Book = choosenBook;
                newBook.User = user;
                user.UserBooks.Add(newBook);
                db.Entry(u).State= EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Library");
            }
;        }
    }
}