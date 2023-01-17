using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        private Context context = new Context();
        public ActionResult IstatistikIndex()
        {
            //Toplam Cari
            var toplamCari = context.Carilers.Count().ToString();
            ViewBag.toplamCari1 = toplamCari;

            //Ürün Sayısı
            var urunSayisi = context.Uruns.Count().ToString();
            ViewBag.urunSayisi1 = urunSayisi;

            //Personel Sayısı
            var personelSayisi = context.Personels.Count().ToString();
            ViewBag.personelSayisi1 = personelSayisi;

            //Kategori Sayısı
            var kategoriSayisi = context.Kategoris.Count().ToString();
            ViewBag.kategoriSayisi1 = kategoriSayisi;

            //Toplam Stok
            var toplamStok = context.Uruns.Sum(u => u.Stok).ToString();
            ViewBag.toplamStok1 = toplamStok;

            //Marka Sayısı
            var markaSayisi = (from _markaSayisi in context.Uruns
                               select _markaSayisi.Marka).Distinct().Count().ToString();
            ViewBag.markaSayisi1 = markaSayisi;

            //Kritik Seviye Ürün
            var kritikSeviye = context.Uruns.Count(u => u.Stok < 20).ToString();
            ViewBag.kritikSeviye1 = kritikSeviye;

            //Max Fiyatlı Ürün
            var maxFiyat = (from _maxFiyat in context.Uruns
                            orderby _maxFiyat.SatısFiyat descending
                            select _maxFiyat.UrunAd).FirstOrDefault();
            ViewBag.maxFiyat1 = maxFiyat;

            //Min Fiyatlı Ürün
            var minFiyat = (from _minFiyat in context.Uruns
                            orderby _minFiyat.SatısFiyat ascending
                            select _minFiyat.UrunAd).FirstOrDefault();
            ViewBag.minFiyat1 = minFiyat;

            //Max Marka maxMarka1
            var maxMarka = context.Uruns.GroupBy(u => u.Marka).OrderByDescending(m => m.Count())
                .Select(m1 => m1.Key).FirstOrDefault();
            ViewBag.maxMarka1 = maxMarka;

            //Buzdolabı Sayısı
            var buzdolabıSayisi = context.Uruns.Count(b => b.UrunAd == "Buzdolabı").ToString();
            ViewBag.buzdolabıSayisi1 = buzdolabıSayisi;

            //Laptop Sayısı
            var laptopSayisi = context.Uruns.Count(l => l.UrunAd == "Laptop").ToString();
            ViewBag.laptopSayisi1 = laptopSayisi;

            //En Çok Satan
            var cokSatan = context.Uruns.Where(u => u.UrunID == (context.SatisHarekets.GroupBy(sh => sh.Urunid).OrderByDescending(ui => ui.Count())
                .Select(sh1 => sh1.Key).FirstOrDefault())).Select(ua => ua.UrunAd).FirstOrDefault();
            ViewBag.cokSatan1 = cokSatan;

            //Kasadaki Tutar
            var kasaTutar = context.SatisHarekets.Sum(sh => sh.ToplamTutar).ToString();
            ViewBag.kasaTutar1 = kasaTutar;

            //Bugünkü Satışlar
            DateTime today = DateTime.Today;
            var bugünSatis = context.SatisHarekets.Count(sh => sh.Tarih == today).ToString();
            ViewBag.bugünSatis1 = bugünSatis;

            //Bu Günkü Tutar
            var bugünTutar = context.SatisHarekets.Where(sh => sh.Tarih == today).Sum(sh1 =>(decimal?)sh1.ToplamTutar).ToString();
            ViewBag.bugünTutar1 = bugünTutar;

            return View();
        }
        public ActionResult EasyTables()
        {
            var sorgu = from cariSehir in context.Carilers
                        group cariSehir by cariSehir.CariSehir
                into grup
                        select new SinifGrup
                        {
                            Sehir = grup.Key,
                            Sayi = grup.Count()
                        };
            return View(sorgu.ToList());

        }

        public PartialViewResult PartialDepartman()
        {
            var sorgu2 = from personel in context.Personels
                         group personel by personel.Departman.DepartmanAd into grup2
                         select new SinifGrup2
                         {
                             Departman = grup2.Key,
                             depSayi = grup2.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult PartialCariler()
        {
            var sorgu3 = context.Carilers.ToList();
            return PartialView(sorgu3);
        }
        public PartialViewResult PartialUrunler()
        {
            var sorgu4 = context.Uruns.ToList();
            return PartialView(sorgu4);
        }

        public PartialViewResult PartialMarka()
        {
            var sorgu5 = from marka in context.Uruns
                         group marka by marka.Marka
                into grup2
                         select new SinifGrup3
                         {
                             marka = grup2.Key,
                             sayi = grup2.Count()
                         };
            return PartialView(sorgu5.ToList());
        }
    }
}