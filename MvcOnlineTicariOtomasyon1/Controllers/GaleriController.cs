using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        private Context context = new Context();
        public ActionResult GaleriIndex()
        {
            var resimler = context.Uruns.ToList();
            return View(resimler);
        }
    }
}