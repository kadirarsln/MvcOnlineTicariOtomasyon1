using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class SatisHareketController : Controller
    {
        // GET: SatisHareket
        private Context context = new Context();
        public ActionResult SatisHareketIndex()
        {
            var satislar = context.SatisHarekets.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult SatisHareketAdd()
        {
            List<SelectListItem> urunlerList = (from u in context.Uruns.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = u.UrunAd,
                                                    Value = u.UrunID.ToString()
                                                }).ToList();

            List<SelectListItem> carilerList = (from c in context.Carilers.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = c.CariAd + " " + c.CariSoyad,
                                                    Value = c.CariID.ToString()
                                                }).ToList();

            List<SelectListItem> personelList = (from p in context.Personels.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.PersonelAd + " " + p.PersonelSoyad,
                                                     Value = p.PersonelID.ToString()
                                                 }).ToList();

            ViewBag.personelList1 = personelList;
            ViewBag.cariList1 = carilerList;
            ViewBag.urunList1 = urunlerList;
            return View();
        }

        [HttpPost]
        public ActionResult SatisHareketAdd(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("SatisHareketIndex");
        }

        public ActionResult SatisHareketGet(int id)
        {
            List<SelectListItem> urunlerList = (from u in context.Uruns.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = u.UrunAd,
                                                    Value = u.UrunID.ToString()
                                                }).ToList();

            List<SelectListItem> carilerList = (from c in context.Carilers.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = c.CariAd + " " + c.CariSoyad,
                                                    Value = c.CariID.ToString()
                                                }).ToList();

            List<SelectListItem> personelList = (from p in context.Personels.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.PersonelAd + " " + p.PersonelSoyad,
                                                     Value = p.PersonelID.ToString()
                                                 }).ToList();

            ViewBag.personelList1 = personelList;
            ViewBag.cariList1 = carilerList;
            ViewBag.urunList1 = urunlerList;
            var satisHareketGet = context.SatisHarekets.Find(id);
            return View("SatisHareketGet", satisHareketGet);
        }

        public ActionResult SatisHareketUpdate(SatisHareket satisHareket)
        {
            var update = context.SatisHarekets.Find(satisHareket.SatısID);
            update.Urunid = satisHareket.Urunid;
            update.Cariid = satisHareket.Cariid;
            update.Personelid = satisHareket.Personelid;
            update.Adet= satisHareket.Adet;
            update.Fiyat= satisHareket.Fiyat;
            update.ToplamTutar= satisHareket.ToplamTutar;
            update.Tarih = satisHareket.Tarih;
            context.SaveChanges();
            return RedirectToAction("SatisHareketIndex");
        }

        public ActionResult SatisHareketDetay(int id)
        {
            var satisHareketDetay = context.SatisHarekets.Where(sh => sh.SatısID == id).ToList();
            return View(satisHareketDetay);
        }
    }
}