using FurBuuks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

        [HttpGet]
        public ActionResult RePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RePassword(string email)
        {
            var user = db.Users.Where(x => x.Email == email).FirstOrDefault();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("furbuuks@gmail.com", "Aa123456!");

            var rePassword = ResetPassword();
            MailMessage mm = new MailMessage("furbuuks@gmail.com", email, "Şifre Yenileme", "Yeni şifreniz : " + rePassword);
            user.Password = rePassword;
            db.SaveChanges();
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
            return RedirectToAction("Index");
        }

        Random random = new Random();
        private string ResetPassword()
        {
            string password = "";
            int r;
            for (int i = 0; i < 10; i++)
            {
                r = random.Next(1, 10);
                password += r.ToString();
            }
            return password;
        }
    }
}