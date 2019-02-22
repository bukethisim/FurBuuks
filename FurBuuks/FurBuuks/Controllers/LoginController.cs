using FurBuuks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurBuuks.Controllers
{
    public class LoginController : Controller
    {
        FurBuuksContext db = new FurBuuksContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(User u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(u);
        //        db.SaveChanges();
        //        return Redirect("/User/UserEdit/" + u.Id);
        //    }

        //    return View();
        //}

        [HttpPost]
        public JsonResult ValidateUser(string email, string password)
        {
            var data = from c in db.Users where (c.Email == email || c.UserName == email) && c.Password == password select c;
            var selected = data.FirstOrDefault();
            if (data.Count() > 0)
            {
                Session["User"] = selected;
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UserSignUp(User user, string email, string password, string username, string passwordCheck)
        {
            if (password == passwordCheck)
            {
                var data = from u in db.Users where u.Email == email || u.UserName == username select u;
                if (!(data.Count() > 0))
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["User"] = user;
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var emails = from u in db.Users where u.Email == email select u.Email;
                    //var usernames = from u in db.Users where u.UserName == username select u.UserName;
                    if (emails.Contains(email))
                    {
                        return Json(new { Success = false, Error = "Bu email zaten kullanılıyor." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Success = false, Error = "Bu kullanıcı adı zaten kullanılıyor." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
                return Json(new { Success = false, Error = "Girdiğiniz şifreler uyuşmuyor." }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RePassword()
        {
            return View();
        }
    }
}