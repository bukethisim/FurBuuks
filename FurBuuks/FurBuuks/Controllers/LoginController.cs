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
        public JsonResult ValidateUser(string email, string password)
        {
            var data = from c in db.Users where c.SMLinks.Email == email && c.Password == password select c;
            if (data.Count() > 0)
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}