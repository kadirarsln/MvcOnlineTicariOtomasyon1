using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    [AllowAnonymous]   // buradan muhaf tutuyor authorize 
    public class LoginController : Controller
    {
        // GET: Login
        Context context = new Context();
        public ActionResult LoginIndex()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult CariKayitPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CariKayitPartial(Cariler cariler)
        {
            context.Carilers.Add(cariler);
            context.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult CariLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin(Cariler cariler1)
        {
            var cariGiris = context.Carilers.FirstOrDefault(c =>
                c.CariMail == cariler1.CariMail && c.CariSifre == cariler1.CariSifre);
            if (cariGiris != null)
            {
                FormsAuthentication.SetAuthCookie(cariGiris.CariMail, false);
                Session["CariMail"] = cariGiris.CariMail.ToString();
                return RedirectToAction("CariPanelIndex", "CariPanel");
            }
            else
            {
                return RedirectToAction("LoginIndex", "Login");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var adminGiris =
                context.Admins.FirstOrDefault(a => a.KullanıcıAdı == admin.KullanıcıAdı && a.Sifre == admin.Sifre);
            if (adminGiris != null)
            {
                FormsAuthentication.SetAuthCookie(adminGiris.KullanıcıAdı, false);
                Session["KullaniciAd"] = adminGiris.KullanıcıAdı.ToString();
                return RedirectToAction("Index", "Kategori1");
            }
            else
            {
                return RedirectToAction("LoginIndex", "Login");
            }
        }
    }
}