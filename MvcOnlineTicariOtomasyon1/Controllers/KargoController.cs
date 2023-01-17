using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        private Context context = new Context();

        public ActionResult KargoIndex(string ara)
        {
            var kargolar = from kargo1 in context.KargoDetays select kargo1; //TRUE olanları listeleme.
            if (!string.IsNullOrEmpty(ara))
            {
                kargolar = kargolar.Where(k =>
                    k.TakipKodu.Contains(ara)); //arama işlemi yaptırıyoruz ürün adına ve markaya göre
            }

            return View(kargolar.ToList());
        }

        //KKARGO EKLE
        [HttpGet]
        public ActionResult KargoAdd()
        {
            //Takip Kodu Oluşturmak için.
            Random random = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F" };
            int k1, k2, k3;
            k1 = random.Next(0, karakterler.Length);
            k2 = random.Next(0, karakterler.Length);
            k3 = random.Next(0, karakterler.Length);

            int s1, s2, s3;
            s1 = random.Next(100, 1000); //10--> 3 1 2 1 2 1
            s2 = random.Next(10, 99);
            s3 = random.Next(10, 99);

            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            return View();
        }

        [HttpPost]
        public ActionResult KargoAdd(KargoDetay kargoDetay)
        {
            context.KargoDetays.Add(kargoDetay);
            context.SaveChanges();
            return RedirectToAction("KargoIndex");
        }

        public ActionResult KargoTakip(string id) //route config id olmalıdır aralarında geçiş için.!!!
        {
            var kargoTakip = context.KargoTakips.Where(kt => kt.TakipKodu == id).ToList();
            return View(kargoTakip);
        }
    }
}