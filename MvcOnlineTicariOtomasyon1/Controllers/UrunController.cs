using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context context = new Context();
        public ActionResult UrunIndex(string ara)
        {
            var urunler = from urun1 in context.Uruns select urun1;      //TRUE olanları listeleme.
            if (!string.IsNullOrEmpty(ara))
            {
                urunler = urunler.Where(u => u.UrunAd.Contains(ara) || u.Marka.Contains(ara));   //arama işlemi yaptırıyoruz ürün adına ve markaya göre
            }
            return View(urunler.ToList());
        }

        //Ürün Ekleme
        [HttpGet]
        public ActionResult UrunAdd()
        {
            List<SelectListItem> kategoriList = (from k in context.Kategoris.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = k.KategoriAd,                        //Dropdown yapısı kullandık listeleme şeklinde kategori seçimi yapabilşmek için.
                                                     Value = k.KategoriID.ToString()
                                                 }).ToList();

            ViewBag.kategoriList1 = kategoriList;           //Controllerdan view tarafına değer ve veri taşırız.
            return View();
        }
        [HttpPost]
        public ActionResult UrunAdd(Urun urun)
        {
            context.Uruns.Add(urun);
            context.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        //ÜRÜN SİLME
        public ActionResult UrunDelete(int id)                  //Silme işleminde false true DURUM yardımı ile kodladık. True olan ürünleri listeleyeceğiz burdan silinen ürünleri false yaptık.
        {
            var delete = context.Uruns.Find(id);
            delete.Durum = false;
            context.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        //ÜRÜN GÜNCELLEME
        public ActionResult UrunGet(int id)
        {
            List<SelectListItem> kategoriList = (from k in context.Kategoris.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = k.KategoriAd,                        //Dropdown yapısı kullandık listeleme şeklinde kategori seçimi yapabilşmek için.
                                                     Value = k.KategoriID.ToString()
                                                 }).ToList();

            ViewBag.kategoriList1 = kategoriList;           //Controllerdan view tarafına değer ve veri taşırız.

            var urunGet = context.Uruns.Find(id);
            return View("UrunGet", urunGet);
        }
        public ActionResult UrunUpdate(Urun urun)
        {
            var update = context.Uruns.Find(urun.UrunID);       //ID hafızaya aldık.ID ye göre işlem gerçekleştirdik.
            update.UrunAd = urun.UrunAd;                        //Update değerini girdiğimizle değiştiriyoruz.sol taraf atancak değer sağ taraf yeni değer.
            update.Marka = urun.Marka;
            update.Stok = urun.Stok;
            update.AlısFiyat = urun.AlısFiyat;
            update.SatısFiyat = urun.SatısFiyat;
            update.Kategoriid = urun.Kategoriid;
            update.Durum = urun.Durum;
            update.UrunGorsel = urun.UrunGorsel;

            context.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        public ActionResult UrunList()
        {
            var urunList = context.Uruns.ToList();
            return View(urunList);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> personelList = (from p in context.Personels.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.PersonelAd + " " + p.PersonelSoyad,
                                                     Value = p.PersonelID.ToString()
                                                 }).ToList();

            ViewBag.personelList1 = personelList;
            var urunlerList = context.Uruns.Find(id);
            ViewBag.urunList1 = urunlerList.UrunID;
            ViewBag.satisFiyat = urunlerList.SatısFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("SatisHareketIndex","SatisHareket");
        }
    }
}