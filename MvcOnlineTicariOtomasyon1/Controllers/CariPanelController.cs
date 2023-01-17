using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context context = new Context();
        [Authorize]
        public ActionResult CariPanelIndex()
        {
            var mail = (string)Session["CariMail"];
            var cariler = context.Mesajlars.Where(m => m.Alici == mail).ToList();
            ViewBag.mail1 = mail;

            var mailid = context.Carilers.Where(c => c.CariMail == mail).Select(c1 => c1.CariID).FirstOrDefault();
            ViewBag.mailid1 = mailid;

            var toplamsatis = context.SatisHarekets.Where(c => c.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;

            var toplamtutar = context.SatisHarekets.Where(sh => sh.Cariid == mailid).Sum(sh1 => sh1.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;

            var toplamurun = context.SatisHarekets.Where(sh => sh.Cariid == mailid).Sum(sh1 => sh1.Adet);
            ViewBag.toplamurun = toplamurun;
            
            var adsoyad = context.Carilers.Where(a => a.CariMail == mail).Select(c1 => c1.CariAd + " " + c1.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(cariler);

        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];                                 //Sisteme giriş yapan mail  adresli kişinin ID atamasını yaptık.
            var id = context.Carilers.Where(c => c.CariMail == mail.ToString()).Select(c1 => c1.CariID)
                .FirstOrDefault();
            var siparisler = context.SatisHarekets.Where(sh => sh.Cariid == id).ToList();
            return View(siparisler);
        }

        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];   //Sisteme giriş yapan mail.
            var gelenmesajlar = context.Mesajlars.Where(m => m.Alici == mail).OrderByDescending(m => m.MesajID).ToList();     //Aliciya gelen mailler yazıyor.

            var gelenmesajsayisi = context.Mesajlars.Count(m => m.Alici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gelenmesajsayisi1 = gelenmesajsayisi;

            var gidenmesajsayisi = context.Mesajlars.Count(m => m.Gönderici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gidenmesajsayisi1 = gidenmesajsayisi;
            return View(gelenmesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];   //Sisteme giriş yapan mail.
            var gidenmesajlar = context.Mesajlars.Where(m => m.Gönderici == mail).OrderByDescending(m => m.MesajID).ToList();     //Aliciya gelen mailler yazıyor.

            var gelenmesajsayisi = context.Mesajlars.Count(m => m.Alici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gelenmesajsayisi1 = gelenmesajsayisi;

            var gidenmesajsayisi = context.Mesajlars.Count(m => m.Gönderici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gidenmesajsayisi1 = gidenmesajsayisi;
            return View(gidenmesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var mesajDetay = context.Mesajlars.Where(md => md.MesajID == id).ToList();
            var gelenmesajsayisi = context.Mesajlars.Count(m => m.Alici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gelenmesajsayisi1 = gelenmesajsayisi;

            var gidenmesajsayisi = context.Mesajlars.Count(m => m.Gönderici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gidenmesajsayisi1 = gidenmesajsayisi;
            return View(mesajDetay);
        }
        public ActionResult MesajDetay2(int id)
        {
            var mail = (string)Session["CariMail"];
            var mesajDetay2 = context.Mesajlars.Where(md => md.MesajID == id).ToList();
            var gelenmesajsayisi = context.Mesajlars.Count(m2 => m2.Alici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gelenmesajsayisi1 = gelenmesajsayisi;

            var gidenmesajsayisi = context.Mesajlars.Count(m2 => m2.Gönderici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gidenmesajsayisi1 = gidenmesajsayisi;
            return View(mesajDetay2);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelenmesajsayisi = context.Mesajlars.Count(m => m.Alici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gelenmesajsayisi1 = gelenmesajsayisi;

            var gidenmesajsayisi = context.Mesajlars.Count(m => m.Gönderici == mail).ToString();   //Mesaj sayısını gösteriyoruz.
            ViewBag.gidenmesajsayisi1 = gidenmesajsayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesajlar)
        {
            var mail = (string)Session["CariMail"];
            mesajlar.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesajlar.Gönderici = mail;
            context.Mesajlars.Add(mesajlar);
            context.SaveChanges();

            return View();
        }
        public ActionResult KargoTakipCari(string ara)
        {
            var kargolar = from kargo1 in context.KargoDetays select kargo1; //TRUE olanları listeleme.

            kargolar = kargolar.Where(k =>
                k.TakipKodu.Contains(ara)); //arama işlemi yaptırıyoruz ürün adına ve markaya göre
            return View(kargolar.ToList());
        }

        public ActionResult KargoTakip1(string id)
        {
            var kargoTakip = context.KargoTakips.Where(kt => kt.TakipKodu == id).ToList();
            return View(kargoTakip);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            
            return RedirectToAction("LoginIndex","Login");
        }

        public PartialViewResult PartialAyarlarResult()
        {
            var mail = (string)Session["CariMail"];
            var id = context.Carilers.Where(c => c.CariMail == mail).Select(c1 => c1.CariID).FirstOrDefault();
            var caribul = context.Carilers.Find(id);
            return PartialView("PartialAyarlarResult",caribul);
        }

        public ActionResult CariUpdate(Cariler cari)
        {
            var update = context.Carilers.Find(cari.CariID);       
            update.CariAd = cari.CariAd;                       
            update.CariSoyad = cari.CariSoyad;                       
            update.CariSifre = cari.CariSifre;                       
            update.CariMail = cari.CariMail;                       
            context.SaveChanges();
            return RedirectToAction("CariPanelIndex");
        }
    }
}