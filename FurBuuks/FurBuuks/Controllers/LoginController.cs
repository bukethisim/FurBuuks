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

        [HttpPost]
        public ActionResult Index(User u)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(u);
                RedirectToAction("UserEdit", "User");
            }

            return View();
        }

        [HttpPost]
        public JsonResult ValidateUser(string email, string password)
        {
            var data = from c in db.Users where c.Email == email && c.Password == password select c;
            var selected = data.FirstOrDefault();
            if (data.Count() > 0)
            {
                Session["User"] = selected;
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}