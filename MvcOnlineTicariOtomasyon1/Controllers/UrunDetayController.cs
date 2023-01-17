using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context context = new Context();
        public ActionResult UrunDetayIndex()
        {
            ClassDetay classDetay = new ClassDetay();
            //var urunDetaylar = context.Uruns.Where(ud => ud.UrunID == 1).ToList();
            classDetay.Urun1 = context.Uruns.Where(cd1 => cd1.UrunID == 1).ToList();
            classDetay.Urun2 = context.Detays.Where(cd2 => cd2.DetayID == 1).ToList();
            return View(classDetay);
        }
    }
}