using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        private Context context = new Context();
        public ActionResult FaturaIndex()
        {
            var faturalar = context.Faturalars.ToList();
            return View(faturalar);
        }

        // EKLEME İŞLEMİ
        [HttpGet]
        public ActionResult FaturaAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaAdd(Faturalar faturalar)
        {
            context.Faturalars.Add(faturalar);
            context.SaveChanges();
            return RedirectToAction("FaturaIndex");
        }

        //GETİRME VE GÜNCELLEME İŞLEMİ
        public ActionResult FaturaGet(int id)
        {
            var faturaGet = context.Faturalars.Find(id);
            return View("FaturaGet", faturaGet);
        }

        public ActionResult FaturaUpdate(Faturalar faturalar)
        {
            var update = context.Faturalars.Find(faturalar.FaturaID);
            update.FaturaSeriNo = faturalar.FaturaSeriNo;
            update.FaturaSıraNo = faturalar.FaturaSıraNo;
            update.VergiDairesi = faturalar.VergiDairesi;
            update.Tarih = faturalar.Tarih;
            update.Saat = faturalar.Saat;
            update.TeslimEden = faturalar.TeslimEden;
            update.TeslimAlan = faturalar.TeslimAlan;
            context.SaveChanges();
            return RedirectToAction("FaturaIndex");
        }

        //FATURA DETAY
        public ActionResult FaturaDetay(int id)
        {
            var faturaDetay = context.FaturaKalems.Where(f => f.Faturaid == id).ToList();
            var faturaDetay2 = context.Faturalars.Where(f => f.FaturaID == id).Select(f2 => f2.FaturaSeriNo)
                .FirstOrDefault();
            ViewBag.fd = faturaDetay2;
            return View(faturaDetay);
        }

        //FATURA KALEM
        [HttpGet]
        public ActionResult FaturaKalemAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaKalemAdd(FaturaKalem faturaKalem)
        {
            context.FaturaKalems.Add(faturaKalem);
            context.SaveChanges();
            return RedirectToAction("FaturaIndex");
        }

        public ActionResult FaturaDinamik()
        {
            FaturaDinamikClass faturaDinamikClass = new FaturaDinamikClass();
            faturaDinamikClass.fatura1 = context.Faturalars.ToList();
            faturaDinamikClass.fatura2 = context.FaturaKalems.ToList();
            return View(faturaDinamikClass);
        }

        public ActionResult FaturaDinamikAdd(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi,
            string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalems)
        {
            Faturalar faturalar = new Faturalar();
            faturalar.FaturaSeriNo = FaturaSeriNo;
            faturalar.FaturaSıraNo = FaturaSıraNo;
            faturalar.Tarih = Tarih;
            faturalar.VergiDairesi = VergiDairesi;
            faturalar.Saat = Saat;
            faturalar.TeslimEden = TeslimEden;
            faturalar.TeslimAlan = TeslimAlan;
            faturalar.Toplam = decimal.Parse(Toplam);
            context.Faturalars.Add(faturalar);

            foreach (var _kalem in kalems)
            {
                FaturaKalem faturaKalem = new FaturaKalem();
                faturaKalem.Acıklama = _kalem.Acıklama;
                faturaKalem.BrimFiyat = _kalem.BrimFiyat;
                faturaKalem.Faturaid = _kalem.FaturaKalemID;
                faturaKalem.Miktar = _kalem.Miktar;
                faturaKalem.Tutar = _kalem.Tutar;
                context.FaturaKalems.Add(faturaKalem);
            }
            context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);

        }
    }
}