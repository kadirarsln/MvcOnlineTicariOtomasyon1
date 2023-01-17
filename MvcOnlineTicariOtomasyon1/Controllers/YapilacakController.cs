using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        private Context context = new Context();
        public ActionResult YapilacakIndex()
        {
            var cariYapilacak = context.Carilers.Count().ToString();
            ViewBag.cariYapilacak1 = cariYapilacak;

            var urunYapilacak = context.Uruns.Count().ToString();
            ViewBag.urunYapilacak1 = urunYapilacak;

            var kategoriYapilacak = context.Kategoris.Count().ToString();
            ViewBag.kategoriYapilacak1 = kategoriYapilacak;

            var cariSehirYapilacak = (from cariSehir in context.Carilers select cariSehir.CariSehir).Distinct().Count()
                .ToString();
            ViewBag.cariSehirYapilacak1 = cariSehirYapilacak;


            var yapilacaklar = context.Yapilacaks.ToList();

            return View(yapilacaklar);
        }
    }
}