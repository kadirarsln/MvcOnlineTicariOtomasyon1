using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        private Context context = new Context();
        public ActionResult CariIndex()
        {
            var cariler = context.Carilers.Where(c => c.Durum == true).ToList();
            return View(cariler);
        }

        //CARİ EKLEME
        [HttpGet]
        public ActionResult CariAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariAdd(Cariler cariler)
        {
            context.Carilers.Add(cariler);
            context.SaveChanges();
            return RedirectToAction("CariIndex");
        }

        //CARİ SİLME
        public ActionResult CariDelete(int id)                  //Silme işleminde false true DURUM yardımı ile kodladık. True olan ürünleri listeleyeceğiz burdan silinen ürünleri false yaptık.
        {
            var delete = context.Carilers.Find(id);
            delete.Durum = false;
            context.SaveChanges();
            return RedirectToAction("CariIndex");
        }

        //CARİ GÜNCELLEME
        public ActionResult CariGet(int id)
        {
            var cariGet = context.Carilers.Find(id);
            return View("CariGet", cariGet);
        }

        public ActionResult CariUpdate(Cariler cariler)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGet");
            }
            var update = context.Carilers.Find(cariler.CariID);
            update.CariAd = cariler.CariAd;
            update.CariSoyad = cariler.CariSoyad;
            update.CariSehir = cariler.CariSehir;
            update.CariMail = cariler.CariMail;
            update.Durum = cariler.Durum;

            context.SaveChanges();
            return RedirectToAction("CariIndex");
        }

        //CARİ SATIŞLAR
        public ActionResult CariSatis(int id)
        {
            var cariSatis = context.SatisHarekets.Where(cs => cs.Cariid == id).ToList();
            var cariSatis2 = context.Carilers.Where(cs => cs.CariID == id)
                .Select(cs2 => cs2.CariAd + " " + cs2.CariSoyad).FirstOrDefault();
            ViewBag.cari = cariSatis2;
            return View(cariSatis);
        }
    }
}