using MvcOnlineTicariOtomasyon1.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult GrafikIndex()
        {
            return View();
        }
        public ActionResult GrafikIndex2()
        {
            var grafikciz = new Chart(800, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler",
                xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();

            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context context = new Context();
        public ActionResult GrafikIndex3()    //Veri tabanından grafik gösterimi için veri çektik.  Jpeg olaraktan.
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var sonuclar = context.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xValue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y => yValue.Add(y.Stok));

            var grafik = new Chart(width: 1000, height: 700)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xValue, yValues: yValue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult GrafikIndex4()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }

        public List<Class1> UrunListesi()
        {
            List<Class1> Class1s = new List<Class1>();
            Class1s.Add(new Class1()
            {
                urunad = "Bilgisayar",
                stok = 120
            });
            Class1s.Add(new Class1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            Class1s.Add(new Class1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            Class1s.Add(new Class1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            Class1s.Add(new Class1()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });
            return Class1s;
        }           //Manuel olarak google chart kullanımı

        public ActionResult GrafikIndex5()   
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<Class2> UrunListesi2()
        {
            List<Class2> Class2s = new List<Class2>();
            using (var context = new Context())
            {
                Class2s = context.Uruns.Select(u => new Class2
                {
                    urn = u.UrunAd,
                    stk = u.Stok
                }).ToList();
            }
            return Class2s;
        }          //Veri tabanından grafik için veri çektik.Google chart kullanımı

        public ActionResult GrafikIndex6()
        {
            return View();
        }

        public ActionResult GrafikIndex7()
        {
            return View();
        }
    }
}
